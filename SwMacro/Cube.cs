using System;
using System.Collections.Generic;
using System.Text;
using SolidWorks.Interop.sldworks;

namespace Macro3.csproj
{
    class Cube
    {
        public List<Point> cubeCoord;
        public Cube(double x0,double y0,double z0,double x1,double y1,double z1)
        {
            cubeCoord = new List<Point>();
            cubeCoord.Add(new Point(x0 / 100, y0 / 100, z0 / 100));
            cubeCoord.Add(new Point(x0 / 100, y1 / 100, z0 / 100));
            cubeCoord.Add(new Point(x0 / 100, y0 / 100, z1 / 100));
            cubeCoord.Add(new Point(x0 / 100, y1 / 100, z1 / 100));
            cubeCoord.Add(new Point(x1 / 100, y0 / 100, z0 / 100));
            cubeCoord.Add(new Point(x1 / 100, y1 / 100, z0 / 100));
            cubeCoord.Add(new Point(x1 / 100, y0 / 100, z1 / 100));
            cubeCoord.Add(new Point(x1 / 100, y1 / 100, z1 / 100));
        }

        public void Draw(IModelDoc2 swModel)
        {
            swModel.CreateLine2(cubeCoord[0].x, cubeCoord[0].y, cubeCoord[0].z, cubeCoord[1].x, cubeCoord[1].y, cubeCoord[1].z);
            swModel.CreateLine2(cubeCoord[0].x, cubeCoord[0].y, cubeCoord[0].z, cubeCoord[4].x, cubeCoord[4].y, cubeCoord[4].z);
            swModel.CreateLine2(cubeCoord[1].x, cubeCoord[1].y, cubeCoord[1].z, cubeCoord[5].x, cubeCoord[5].y, cubeCoord[5].z);
            swModel.CreateLine2(cubeCoord[4].x, cubeCoord[4].y, cubeCoord[4].z, cubeCoord[5].x, cubeCoord[5].y, cubeCoord[5].z);

            swModel.CreateLine2(cubeCoord[2].x, cubeCoord[2].y, cubeCoord[2].z, cubeCoord[6].x, cubeCoord[6].y, cubeCoord[6].z);
            swModel.CreateLine2(cubeCoord[2].x, cubeCoord[2].y, cubeCoord[2].z, cubeCoord[3].x, cubeCoord[3].y, cubeCoord[3].z);
            swModel.CreateLine2(cubeCoord[7].x, cubeCoord[7].y, cubeCoord[7].z, cubeCoord[6].x, cubeCoord[6].y, cubeCoord[6].z);
            swModel.CreateLine2(cubeCoord[7].x, cubeCoord[7].y, cubeCoord[7].z, cubeCoord[3].x, cubeCoord[3].y, cubeCoord[3].z);

            swModel.CreateLine2(cubeCoord[2].x, cubeCoord[2].y, cubeCoord[2].z, cubeCoord[0].x, cubeCoord[0].y, cubeCoord[0].z);
            swModel.CreateLine2(cubeCoord[6].x, cubeCoord[6].y, cubeCoord[6].z, cubeCoord[4].x, cubeCoord[4].y, cubeCoord[4].z);
            swModel.CreateLine2(cubeCoord[7].x, cubeCoord[7].y, cubeCoord[7].z, cubeCoord[5].x, cubeCoord[5].y, cubeCoord[5].z);
            swModel.CreateLine2(cubeCoord[3].x, cubeCoord[3].y, cubeCoord[3].z, cubeCoord[1].x, cubeCoord[1].y, cubeCoord[1].z);
        }
    }
    struct Point
    {
        public double x;
        public double y;
        public double z;
        public Point(double _x, double _y, double _z)
        {
            x = _x;
            z = _z;
            y = _y;
        }
    }
}
