using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;

namespace Macro3.csproj
{
    public partial class SolidWorksMacro
    {
        public void Main()
        {
            ModelDoc2 swModel = (ModelDoc2)swApp.ActiveDoc;
            IPartDoc part = (PartDoc)swModel;
            double[] coord = (double[])part.GetPartBox(false);
            swModel.SetAddToDB(true);
            swModel.SetDisplayWhenAdded(false);
            swModel.Insert3DSketch2(false);

            DrawCubes(swModel, coord, part);

            swModel.Insert3DSketch2(true);
            swModel.ClearSelection2(true);
            swModel.SetAddToDB(false);
            swModel.SetDisplayWhenAdded(true);
        }

        private void DrawCubes(ModelDoc2 swModel, double[] pointsArray, IPartDoc part)
        {
            object[] bodies = (object[])part.GetBodies2((int)swBodyType_e.swAllBodies, false);//get body from part
            Body2[] body = new Body2[1];
            body[0] = (Body2)bodies[0];
            
            double[] rayVectorOrigins;
            double[] rayVectorDirections = { 0, 0, 1 };
            double[] rayVectorDirections2 = { 0, 0, -1 };

            Cube cube = new Cube(pointsArray[0], pointsArray[1], pointsArray[2], pointsArray[3], pointsArray[4], pointsArray[5]);//the biggest cube
            cube.Draw(swModel);
           
            bool drawFlag = true;
            double x_dist = (pointsArray[3] - pointsArray[0]) / 10;//divide big cube into 1000 little cubies
            double y_dist = (pointsArray[4] - pointsArray[1]) / 10;
            double z_dist = (pointsArray[5] - pointsArray[2]) / 10;
           
            for (int z = 0; z < 10; z++)
                for (int y = 0; y < 10; y++)
                    for (int x = 0; x < 10; x++)
                    {
                        Cube c = new Cube(pointsArray[0] + x * x_dist, //new little cube
                                          pointsArray[1] + y * y_dist, 
                                          pointsArray[2] + z * z_dist, 
                                          pointsArray[0] + (x + 1) * x_dist,
                                          pointsArray[1] + (y + 1) * y_dist,
                                          pointsArray[2] + (z + 1) * z_dist);

                        for (int i = 0; i < c.cubeCoord.Count; i++)
                        {
                            rayVectorOrigins = new double[3] { c.cubeCoord[i].x, c.cubeCoord[i].y, c.cubeCoord[i].z };

                            int count = (int)swModel.RayIntersections(body,//get number of intersactions of vector
                                                (object)rayVectorOrigins,  //[rayVectorOrigins,rayVectorDirections] with part
                                                (object)rayVectorDirections,
                                                (int)(swRayPtsOpts_e.swRayPtsOptsTOPOLS | swRayPtsOpts_e.swRayPtsOptsNORMALS),
                                                (double).0000001, (double).0000001);
                            int count2 = (int)swModel.RayIntersections(body,
                                                (object)rayVectorOrigins,
                                                (object)rayVectorDirections2,
                                                (int)(swRayPtsOpts_e.swRayPtsOptsTOPOLS | swRayPtsOpts_e.swRayPtsOptsNORMALS),
                                                (double).0000001, (double).0000001);

                            if (count % 2 == 0 && count2 % 2 == 0)
                            {
                                drawFlag = false;
                                break;
                            }
                        }
                        if (drawFlag)//if at least one of cube vertex is not in cube => don`t draw it
                            c.Draw(swModel);
                        drawFlag=true;
                    }

        }

        /// <summary>
        ///  The SldWorks swApp variable is pre-assigned for you.
        /// </summary>
        public SldWorks swApp;
    }
}


