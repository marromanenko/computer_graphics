import numpy as np
from scipy.special import comb
import math

def bernstein_poly(i, n, t):
    return comb(n, i) * ( t**(n-i) ) * (1 - t)**i


def bezier_curve(points, nTimes=1000):
    nPoints = len(points)
    xPoints = np.array([p[0] for p in points])
    yPoints = np.array([p[1] for p in points])

    t = np.linspace(0.0, 1.0, nTimes)

    polynomial_array = np.array([ bernstein_poly(i, nPoints-1, t) for i in range(0, nPoints)   ])

    xvals = np.dot(xPoints, polynomial_array)
    yvals = np.dot(yPoints, polynomial_array)

    return xvals, yvals


if __name__ == "__main__":
    c=math.cos(math.pi/2)
    s=math.sin(math.pi/2)
    from matplotlib import pyplot as plt
    plt.figure(figsize=(10,10))
    points = [[131.00214867, 63.64784003], [126.78469666, 99.05502709], [70.26721926, 80.96616881], [66.32268192, 34.44803489]]
    for nr in range(len(points)):
        plt.text(points[nr][0], points[nr][1], nr)
    xpoints = [p[0] for p in points]
    ypoints = [p[1] for p in points]

    xvals, yvals = bezier_curve(points, nTimes=1000)
    plt.plot(xvals, yvals)
    plt.plot(xpoints, ypoints, "ro")

    points = [[131.00214867, -63.64784003], [126.78469666, -99.05502709], [70.26721926, -80.96616881], [66.32268192, -34.44803489]]
    xpoints = [p[0] for p in points]
    ypoints = [p[1] for p in points]

    xvals, yvals = bezier_curve(points, nTimes=1000)
    plt.plot(xvals, yvals)

    points = [[131.00214867*c-63.64784003*s, 63.64784003*c+131.00214867*s], [126.78469666*c-99.05502709*s, 99.05502709*c+126.78469666*s], [70.26721926*c-80.96616881*s, 80.96616881*c+70.26721926*s], [66.32268192*c-34.44803489*s, 34.44803489*c+66.32268192*s]]
    xpoints = [p[0] for p in points]
    ypoints = [p[1] for p in points]

    xvals, yvals = bezier_curve(points, nTimes=1000)
    plt.plot(xvals, yvals)

    points = [[131.00214867-120, 63.64784003-40], [126.78469666-120, 99.05502709-40], [70.26721926-120, 80.96616881-40], [66.32268192-120, 34.44803489-40]]
    xpoints = [p[0] for p in points]
    ypoints = [p[1] for p in points]
    xvals, yvals = bezier_curve(points, nTimes=1000)
    plt.plot(xvals, yvals)

    points = [[131.00214867/2, 63.64784003/2], [126.78469666/2, 99.05502709/2], [70.26721926/2, 80.96616881/2], [66.32268192/2, 34.44803489/2]]
    xpoints = [p[0] for p in points]
    ypoints = [p[1] for p in points]
    xvals, yvals = bezier_curve(points, nTimes=1000)
    plt.plot(xvals, yvals)
    plt.show()
