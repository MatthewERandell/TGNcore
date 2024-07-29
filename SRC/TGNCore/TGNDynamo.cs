using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;

using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using Dynamo.UI.Commands;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;

using TGNCore;
using Dynamo.Controls;
using Dynamo.Logging;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace TGNDynamo
{

    [IsVisibleInDynamoLibrary(false)]
    public class dynHelp
    {
        public static TGNCore.Point pointConverter(Autodesk.DesignScript.Geometry.Point point)
        {
            TGNCore.Point tempPoint = new TGNCore.Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y), Convert.ToInt32(point.Z));
            
            return tempPoint;
        }

    }


    //[IsVisibleInDynamoLibrary(false)]
    //public class dynTRE
    //{
    //    public List<Rig> tre { get; set; }

    //    public Meta Meta { get; set; }

    //    public dynTRE(Meta meta, List<Rig> rigs) 
    //    {
    //        tre = rigs;
    //        this.Meta = meta;
    //    }

    //}


    //[IsVisibleInDynamoLibrary(false)]
    //public class dynRig
    //{
    //    //public Meta meta { get; set; }
    //    //public dynFocuscube focusCube { get; set; }
    //    //public Camerapath cameraPath { get; set; }
    //    //public Camera camera { get; set; }
    //    //public Canvas canvas { get; set; }

    //    [NodeName("Rig")]
    //    [NodeDescription("TGN Rig Cube Dynamo implementation")]
    //    [NodeCategory("TGN.DYN.RIG")]
    //    [IsVisibleInDynamoLibrary(true)]
    //    [MultiReturn(new[] { "rig", "path" })]

    //    public static Dictionary<string, object> Rig(Meta meta, Focuscube focuscube, Camera camera, Canvas canvas)
    //    {
    //        //public dynRig(Meta meta, dynFocuscube focusCube, Camerapath cameraPath, Camera camera, Canvas canvas)
    //        //this.meta = meta;
    //        //this.focusCube = focusCube;
    //        //this.cameraPath = cameraPath;
    //        //this.camera = camera;
    //        //this.canvas = canvas;

    //        //tgn Rig
    //        TGNCore.Rig tgnRig = new TGNCore.Rig(meta, focuscube, camera, canvas);

    //        return new Dictionary<string, object>
    //        {
    //            {"rig", tgnRig},
    //            {"path", polypath }
    //        };
    //    }
    //}

    [IsVisibleInDynamoLibrary(false)]
    public class dynPath
    {
        //public Meta meta { get; set; }
        //public dynFocuscube focusCube { get; set; }
        //public Camerapath cameraPath { get; set; }
        //public Camera camera { get; set; }
        //public Canvas canvas { get; set; }

        [NodeName("Path")]
        [NodeDescription("TGN Camera Path Dynamo implementation")]
        [NodeCategory("TGN.DYN.PATH")]
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[] { "tgnPath", "path" })]

        public static Dictionary<string, object> CameraPath(Autodesk.DesignScript.Geometry.Plane dynPlane, Autodesk.DesignScript.Geometry.Point sPoint,
            Autodesk.DesignScript.Geometry.Point cPoint, Autodesk.DesignScript.Geometry.Point ePoint)
        {
            CoordinateSystem tempCS = CoordinateSystem.ByPlane(dynPlane);

            //set defaults if point sockets are empty
            Autodesk.DesignScript.Geometry.Point tempSPoint;
            if (sPoint == null) { tempSPoint = Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(tempCS, -1, 0 , 0);  }
            else { tempSPoint = Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(tempCS, sPoint.X, sPoint.Y, sPoint.Z); }
            
            Autodesk.DesignScript.Geometry.Point tempCPoint;
            if (cPoint == null) { tempCPoint = Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(tempCS, 0, 0, 1); }
            else { tempCPoint = Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(tempCS, cPoint.X, cPoint.Y, cPoint.Z); }

            Autodesk.DesignScript.Geometry.Point tempEPoint;
            if (ePoint == null) { tempEPoint = Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(tempCS, 1, 0, 0); }
            else { tempEPoint = Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(tempCS, ePoint.X, ePoint.Y, ePoint.Z); }

            List<Autodesk.DesignScript.Geometry.Point> points = new List<Autodesk.DesignScript.Geometry.Point>();
            points.Add(tempSPoint); points.Add(tempCPoint); points.Add(tempEPoint);
            Autodesk.DesignScript.Geometry.PolyCurve polyPath = Autodesk.DesignScript.Geometry.PolyCurve.ByPoints(points);

            
            //explode dyanmoPlane objects
            Autodesk.DesignScript.Geometry.Point dynPoint = dynPlane.Origin;
            Autodesk.DesignScript.Geometry.Vector dynVector = dynPlane.Normal;
            //Convert back to TGN
            //Point
            TGNCore.Point tgnPoint = dynHelp.pointConverter(dynPoint);
            //Axis
            TGNCore.Axis tgnAxis = new TGNCore.Axis(Convert.ToInt32(dynVector.X), Convert.ToInt32(dynVector.Y), Convert.ToInt32(dynVector.Z));
            //Plane
            TGNCore.Plane tgnPlane = new TGNCore.Plane(tgnPoint, tgnAxis);

            //sPoint
            TGNCore.Point tgnSPoint = dynHelp.pointConverter(sPoint);
            //cPoint
            TGNCore.Point tgnCPoint = dynHelp.pointConverter(cPoint);
            //ePoint
            TGNCore.Point tgnEPoint = dynHelp.pointConverter(ePoint);
            //tgn Path
            TGNCore.Camerapath tgnPath = new Camerapath(tgnPlane, tgnSPoint, tgnCPoint, tgnEPoint);

            return new Dictionary<string, object>
            {
                {"tgnPath", tgnPath},
                {"path", polyPath }
            };
        }
    }

    [IsVisibleInDynamoLibrary(false)]
    public class dynFocuscube
    {
        //private Autodesk.DesignScript.Geometry.Plane plane { get; set; }
        //private int width { get; set; }
        //private int depth { get; set; }
        //private int height { get; set; }

        [NodeName("Focus Cube")]
        [NodeDescription("TGN Focus Cube Dynamo implementation")]
        [NodeCategory("TGN.DYN.CUBE")]
        [IsVisibleInDynamoLibrary(true)]
        [MultiReturn(new[] { "focusCube", "cuboid" })]

        public static Dictionary<string, object> Focuscube(Autodesk.DesignScript.Geometry.Plane dynPlane, int width, int depth, int height)
        {
            CoordinateSystem coordinate = CoordinateSystem.ByPlane(dynPlane);
            List<Autodesk.DesignScript.Geometry.Point> points = new List<Autodesk.DesignScript.Geometry.Point>();
            points.Add(Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(coordinate, -width / 2, -height / 2, 0));
            points.Add(Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(coordinate, -width / 2, height / 2, 0));
            points.Add(Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(coordinate, width / 2, height / 2, 0));
            points.Add(Autodesk.DesignScript.Geometry.Point.ByCartesianCoordinates(coordinate, width / 2, -height / 2, 0));
            PolyCurve poly = PolyCurve.ByPoints(points, true);
            Line path = Line.ByStartPointDirectionLength(points[0], dynPlane.Normal, depth);
            Solid cuboid = Solid.BySweep(poly, path);

            //Infer dynamo items back to TGN items
            //explode dyanmoPlane objects
            Autodesk.DesignScript.Geometry.Point dynPoint = dynPlane.Origin;
            Autodesk.DesignScript.Geometry.Vector dynVector = dynPlane.Normal;


            //Point
            TGNCore.Point tgnPoint = new TGNCore.Point(Convert.ToInt32(dynPoint.X), Convert.ToInt32(dynPoint.Y), Convert.ToInt32(dynPoint.Z));
            //Axis
            TGNCore.Axis tgnAxis = new TGNCore.Axis(Convert.ToInt32(dynVector.X), Convert.ToInt32(dynVector.Y), Convert.ToInt32(dynVector.Z));
            //Plane
            TGNCore.Plane tgnPlane = new TGNCore.Plane(tgnPoint, tgnAxis);
            //Focuscube
            TGNCore.Focuscube tgnCube = new Focuscube(tgnPlane, width, depth, height);

            return new Dictionary<string, object>
            {
                {"focusCube", tgnCube},
                {"cuboid", cuboid }
            };
            
        }
    }

    //[IsVisibleInDynamoLibrary(false)]
    //public class dynCamera
    //{
    //    //public Meta meta { get; set; }
    //    //public dynFocuscube focusCube { get; set; }
    //    //public Camerapath cameraPath { get; set; }
    //    //public Camera camera { get; set; }
    //    //public Canvas canvas { get; set; }

    //    [NodeName("Camera")]
    //    [NodeDescription("TGN Camera Dynamo implementation")]
    //    [NodeCategory("TGN.DYN.CAM")]
    //    [IsVisibleInDynamoLibrary(true)]
    //    [MultiReturn(new[] { "tgnCam", "camera" })]

    //    public static Dictionary<string, object> Camera()
    //    {

    //        //explode dyanmoPlane objects
    //        Autodesk.DesignScript.Geometry.Point dynPoint = dynPlane.Origin;
    //        Autodesk.DesignScript.Geometry.Vector dynVector = dynPlane.Normal;

    //        //Convert back to TGN
    //        //Point
    //        TGNCore.Point tgnPoint = dynHelp.pointConverter(dynPoint);
            
    //        //tgn Cam
    //        TGNCore.Camera tgnCam = new Camera();

    //        return new Dictionary<string, object>
    //        {
    //            {"tgnCam", tgnCam},
    //            {"camera", camera }
    //        };
    //    }

    //}

}

