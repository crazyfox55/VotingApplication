import numpy as np
import csv
from scipy.spatial import Voronoi

def voronoi_finite_polygons_2d(vor, radius=None):
    """
    Reconstruct infinite voronoi regions in a 2D diagram to finite
    regions.

    Parameters
    ----------
    vor : Voronoi
        Input diagram
    radius : float, optional
        Distance to 'points at infinity'.

    Returns
    -------
    regions : list of tuples
        Indices of vertices in each revised Voronoi regions.
    vertices : list of tuples
        Coordinates for revised Voronoi vertices. Same as coordinates
        of input vertices, with 'points at infinity' appended to the
        end.

    """

    if vor.points.shape[1] != 2:
        raise ValueError("Requires 2D input")

    new_regions = []
    new_vertices = vor.vertices.tolist()

    center = vor.points.mean(axis=0)
    if radius is None:
        radius = vor.points.ptp().max()

    # Construct a map containing all ridges for a given point
    all_ridges = {}
    for (p1, p2), (v1, v2) in zip(vor.ridge_points, vor.ridge_vertices):
        all_ridges.setdefault(p1, []).append((p2, v1, v2))
        all_ridges.setdefault(p2, []).append((p1, v1, v2))

    # Reconstruct infinite regions
    for p1, region in enumerate(vor.point_region):
        vertices = vor.regions[region]

        if all(v >= 0 for v in vertices):
            # finite region
            new_regions.append(vertices)
            continue

        # reconstruct a non-finite region
        ridges = all_ridges[p1]
        new_region = [v for v in vertices if v >= 0]

        for p2, v1, v2 in ridges:
            if v2 < 0:
                v1, v2 = v2, v1
            if v1 >= 0:
                # finite ridge: already in the region
                continue

            # Compute the missing endpoint of an infinite ridge

            t = vor.points[p2] - vor.points[p1] # tangent
            t /= np.linalg.norm(t)
            n = np.array([-t[1], t[0]])  # normal

            midpoint = vor.points[[p1, p2]].mean(axis=0)
            direction = np.sign(np.dot(midpoint - center, n)) * n
            far_point = vor.vertices[v2] + direction * radius

            new_region.append(len(new_vertices))
            new_vertices.append(far_point.tolist())

        # sort region counterclockwise
        vs = np.asarray([new_vertices[v] for v in new_region])
        c = vs.mean(axis=0)
        angles = np.arctan2(vs[:,1] - c[1], vs[:,0] - c[0])
        new_region = np.array(new_region)[np.argsort(angles)]

        # finish
        new_regions.append(new_region.tolist())

    return new_regions, np.asarray(new_vertices)

# read data points from csv
zipInfo = []
points = []
collisionDict = {}
with open("zip_code_database.csv", "r") as csvfile:
    reader = csv.reader(csvfile, delimiter=",")
    count = 0
    for row in reader:
        if count == 0:
            # column names so skip
            count = 1
            continue
        # database items
        zipCode = str(row[0])
        zipPrimaryCity = str(row[3])
        zipState = str(row[6])
        zipCounty = str(row[7])
        zipTimezone = str(row[8])
        zipCountry = str(row[11])
        zipLatitude = str(row[12])
        zipLongitude = str(row[13])
        
        # include conditions
        zipType = str(row[1]) # standard zips only no unique, military, or po box
        zipDecommissioned = (str(row[2]) == '1') # zip still in use no decommisiioned zips
        ZipPosition = zipLatitude + zipLongitude # used for collisions of same zip position
        if ((zipType == "STANDARD") and (not zipDecommissioned) and (ZipPosition not in collisionDict)):
            collisionDict[ZipPosition] = True
            zipCounty = zipCounty.replace("'", "^")
            zipI = (zipCode, zipPrimaryCity, zipState, zipCounty, zipTimezone, zipCountry, zipLatitude, zipLongitude)
            zipInfo.append(zipI)
            points.append([float(zipLatitude), float(zipLongitude)])
        count+=1

print("Input Reading Done")

# compute Voronoi tesselation
vor = Voronoi(points)

print("Voronoi Done")

# find regions and their vertices using voronoi
regions, vertices = voronoi_finite_polygons_2d(vor)

print("Finite Voronoi Done")
zipCodeColumnNames = ('Zip','PrimaryCity','State','County','Timezone','Country','Latitude','Longitude','Geometry')
file = open("ZipCodeImport.sql", "w")
combined = "CREATE TABLE ZipCode(\n"
combined += str.format("\t{} INTEGER NOT NULL PRIMARY KEY\n", zipCodeColumnNames[0])
combined += str.format("\t,{} VARCHAR(64) NOT NULL\n", zipCodeColumnNames[1])
combined += str.format("\t,{} VARCHAR(2) NOT NULL\n", zipCodeColumnNames[2])
combined += str.format("\t,{} VARCHAR(64) NOT NULL\n", zipCodeColumnNames[3])
combined += str.format("\t,{} VARCHAR(64) NOT NULL\n", zipCodeColumnNames[4])
combined += str.format("\t,{} VARCHAR(2) NOT NULL\n", zipCodeColumnNames[5])
combined += str.format("\t,{} NUMERIC(8,3) NOT NULL\n", zipCodeColumnNames[6])
combined += str.format("\t,{} NUMERIC(8,3) NOT NULL\n", zipCodeColumnNames[7])
combined += str.format("\t,{} VARCHAR(512) NOT NULL\n", zipCodeColumnNames[8])
combined += ");\n"
combined += "SET IDENTITY_INSERT ZipCode ON\n"
zipIndex = 0
maxLen = 0
for region in regions:
    zipGeometry = "["
    for vertIndex in region:
        zipGeometry += str.format("[{}, {}],", round(vertices[vertIndex][1],6), round(vertices[vertIndex][0],6))
    zipGeometry = zipGeometry.strip(',')
    zipGeometry += "]"
    if (len(zipGeometry) > maxLen):
        maxLen = len(zipGeometry)
    zipInfo[zipIndex] = zipInfo[zipIndex]+(zipGeometry,)
    zipInfoInsert = str.format("INSERT INTO ZipCode{} VALUES {};\n", str(zipCodeColumnNames).replace("'",""), zipInfo[zipIndex])
    zipInfoInsert = zipInfoInsert.replace("^", "''")
    combined += zipInfoInsert
    zipIndex += 1
combined += "SET IDENTITY_INSERT ZipCode OFF\n"
print("Max Geometry size took",maxLen,"characters to store.")
file.write(combined)
file.close()
print("Write done")
