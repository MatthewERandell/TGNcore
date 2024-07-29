using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGNCore
{

    public class TRE
    {
        public List<Rig> tre { get; set; }
        public Meta meta { get; set; }
        public TRE(Meta meta, List<Rig> rigs) 
        {
            tre = rigs;
            this.meta = meta;
        }
    }

    public class Rig
    {
        public Meta meta { get; set; }
        public Focuscube focusCube { get; set; }
        public Camerapath cameraPath { get; set; }
        public Camera camera { get; set; }
        public Canvas canvas { get; set; }

        public Rig(Meta meta, Focuscube focusCube, Camerapath cameraPath, Camera camera, Canvas canvas)
        {
            this.meta = meta;
            this.focusCube = focusCube;
            this.cameraPath = cameraPath;
            this.camera = camera;
            this.canvas = canvas;
        }
    }

    public class Meta
    {
        public string tag { get; set; }
        public string approver { get; set; }
        public string originator { get; set; }
        public string revision { get; set; }

        public Meta(string tag, string approver, string originator, string revision)
        {
            this.tag = tag;
            this.approver = approver;
            this.originator = originator;
            this.revision = revision;
        }
    }

    public class Focuscube
    {
        public Plane plane { get; set; }
        public int width { get; set; }
        public int depth { get; set; }
        public int height { get; set; }

        public Focuscube(Plane plane, int width, int depth, int height)
        {
            this.plane = plane;
            this.width = width;
            this.depth = depth;
            this.height = height;
        }
    }

    public class Plane
    {
        public Point point { get; set; }
        public Axis axis { get; set; }

        public Plane(Point point, Axis axis)
        {
            this.point = point;
            this.axis = axis;
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Axis
    {
        public int rX { get; set; }
        public int rY { get; set; }
        public int rZ { get; set; }

        public Axis(int rX, int rY, int rZ)
        {
            this.rX = rX;
            this.rY = rY;
            this.rZ = rZ;
        }
    }

    public class Camerapath
    {
        public Plane oPlane { get; set; }
        public Point sPoint { get; set; }
        public Point cPoint { get; set; }
        public Point ePoint { get; set; }

        public Camerapath(Plane oPlane, Point sPoint, Point cPoint, Point ePoint)
        {
            this.oPlane = oPlane;
            this.sPoint = sPoint;
            this.cPoint = cPoint;
            this.ePoint = ePoint;
        }
    }

    public class Camera
    {
        public Camerapath cPath { get; set; }
        public Point fPoint { get; set; }
        public int focalLength { get; set; }
    
        public Camera(Camerapath cPath, Point fPoint, int focalLength)
        {
            this.cPath = cPath;
            this.fPoint = fPoint;
            this.focalLength = focalLength;
        }
    }

    public class Canvas
    {
        public Plane oPlane { get; set; }
        public int scale { get; set; }
    
        public Canvas(Plane oPlane, int scale)
        {
            this.oPlane = oPlane;
            this.scale = scale;
        }
    }


}
