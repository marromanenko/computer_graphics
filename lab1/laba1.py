from tkinter import *
import time
pixel_size = 5
height = 500
width = 800


class Algorithms:
    my_surname = {
        "R": [[(10, 10), (10, 30)], [(10, 10), (20, 10)], [(20, 10), (20, 20)], [(10, 20), (20, 20)], [(10, 20), (20, 30)]],
        "o1": [[(30, 25), (5, 0)]],
        "m": [[(40, 20), (40, 30)], [(40, 20), (45, 30)], [(45, 30), (50, 20)], [(50, 20), (50, 30)]],
        "a": [[(55, 30), (55, 25)], [(55, 25), (60, 20)], [(60, 20), (65, 25)], [(55, 25), (65, 25)], [(65, 25), (65, 30)]],
        "n1": [[(70, 20), (70, 30)], [(70, 20), (80, 30)], [(80, 20), (80, 30)]],
        "e": [[(85, 20), (85, 30)], [(85, 20), (95, 20)], [(85, 25), (95, 25)], [(85, 30), (95, 30)]],
        "n2": [[(100, 20), (100, 30)], [(100, 20), (110, 30)], [(110, 20), (110, 30)]],
        "k": [[(115, 20), (115, 30)], [(125, 20), (115, 25)], [(115, 25), (125, 30)]],
        "o2": [[(135, 25), (5, 0)]],
        "s": [[(20, 50), (150, 80)]]
    }

    def dda_for_section(self, x1, y1, x2, y2):
        t1 = time.time()
        dx = abs(x2 - x1)
        dy = abs(y2 - y1)
        steps = dx if dx >= dy else dy
        dx = (x2 - x1) / steps
        dy = (y2 - y1) / steps
        x, y = x1, y1
        points = [[x, y], [x2, y2]]
        for i in range(steps - 1):
            x += dx
            y += dy
            points.append([round(x), round(y)])
        self.draw(points)
        t2 = time.time()
        print("DDA for section:")
        print(t2 - t1)


    def bresenham_for_section(self, x1, y1, x2, y2):
        t1 = time.time()
        dx = x2 - x1
        dy = y2 - y1
        is_steep = abs(dy) > abs(dx)
        if is_steep:
            x1, y1 = y1, x1
            x2, y2 = y2, x2
        if x1 > x2:
            x1, x2 = x2, x1
            y1, y2 = y2, y1
        dx = x2 - x1
        dy = y2 - y1
        error = int(dx / 2.0)
        ystep = 1 if y1 < y2 else -1
        y = y1
        points = []
        for x in range(x1, x2 + 1):
            coord = (y, x) if is_steep else (x, y)
            points.append(coord)
            error -= abs(dy)
            if error < 0:
                y += ystep
                error += dx
        self.draw(points)
        t2 = time.time()
        print("Bresenham for section:")
        print(t2 - t1)

    def bresenham_for_circle(self, xc, yc, radius):
        t1 = time.time()
        x = 0
        y = radius
        delta = 1 - 2 * radius
        points = []
        while (y >= 0):
            points.append((xc + x, yc + y))
            points.append((xc + x, yc - y))
            points.append((xc - x, yc + y))
            points.append((xc - x, yc - y))
            error = 2 * (delta + y) - 1
            if (delta < 0) and (error <= 0):
                x += 1
                delta += 2 * x + 1
                continue
            error = 2 * (delta - x) - 1
            if delta > 0 and error > 0:
                y -= 1
                delta += 1 - 2 * y
                continue
            x += 1
            delta += 2 * (x - y)
            y -= 1
        self.draw(points)
        t2 = time.time()
        print("Bresenham for circle:")
        print(t2 - t1)

    def wu_for_section(self, x1, y1, x2, y2):
        t1 = time.time()

        def _fpart(x):
            return x - int(x)

        def _rfpart(x):
            return 1 - _fpart(x)
        points = []
        dx, dy = x2 - x1, y2 - y1
        x, y = x1, y1
        if dy == 0:
            points.append([round(x), round(y1)])
            while abs(x) < abs(x2):
                x += 1
                points.append([round(x), round(y1)])
        elif dx == 0:
            points.append([round(x1), round(y)])
            while abs(y) < abs(y2):
                y += 1
                points.append([round(x1), round(y)])
        else:
            grad = dy / dx
            intery = y1 + _rfpart(x1) * grad

            def draw_endpoint(x, y):
                xend = round(x)
                yend = y + grad * (xend - x)
                px, py = int(xend), int(yend)
                points.append([px, py])
                points.append([px, py + 1])
                return px
            xstart = draw_endpoint(x1, y1)
            xend = draw_endpoint(x2, y2)
            for x in range(xstart, xend):
                y = int(intery)
                points.append([x, y])
                points.append([x, y + 1])
                intery += grad
        self.draw(points)
        t2 = time.time()
        print("Wu for section:")
        print(t2 - t1)

    def draw(self, coords):
        for point in coords:
            self.canvas.create_rectangle(pixel_size * point[0], pixel_size * point[1],
                                         pixel_size * point[0] + pixel_size, pixel_size * point[1] + pixel_size,
                                         fill="medium violet red", tag="my_surname")

    def clean(self):
        self.canvas.delete("my_surname")

    def callback(self, func_name):
        if func_name != "bresenham_for_circle":
            def func():
                for letter, lines in self.my_surname.items():
                    for line in lines:
                        if letter == "o1" or letter == "o2":
                            getattr(self, "bresenham_for_circle")(line[0][0], line[0][1], line[1][0])
                        else:
                            getattr(self, func_name)(line[0][0], line[0][1], line[1][0], line[1][1])
            return func
        return lambda func_name=func_name: getattr(self, func_name)(50, 40, 30)

    def __init__(self):
        window = Tk()
        window.title("Lab work 1 by Romanenko")
        self.canvas = Canvas(window, width=width, height=height, bg="pale violet red")
        self.canvas.pack()
        frame = Frame(window, bg="pale violet red")
        frame.pack()
        dda_btn = Button(frame, text="DDA for section", command=self.callback("dda_for_section"))
        dda_btn.grid(row=1, column=1)
        bres_btn = Button(frame, text="Bresenham for section", command=self.callback("bresenham_for_section"))
        bres_btn.grid(row=1, column=2)
        circle_bres_btn = Button(frame, text="Bresenham for circle", command=self.callback("bresenham_for_circle"))
        circle_bres_btn.grid(row=1, column=3)
        wu_btn = Button(frame, text="Wu for section", command=self.callback("wu_for_section"))
        wu_btn.grid(row=1, column=4)
        clear_btn = Button(frame, text="Clear", command=self.clean)
        clear_btn.grid(row=1, column=5)
        window.mainloop()


if __name__ == "__main__":
    Algorithms()
