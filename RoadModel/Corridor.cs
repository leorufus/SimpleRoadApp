using System;
using System.Globalization;
using System.Xml;
using Xml2CSharp;


namespace RoadModel
{
    public class Corridor
    {
        private Alignment aligment;        

        public Corridor(LandXML data)
        {
            aligment = data.Alignments.Alignment[0];

            double X = 0;
            double Y = 0;

            double sta = 20;

            XYCoordInStation(sta, ref X, ref Y);
        }

        enum CoordGeomType
        {
            line,
            curve,
            spiral
        }

        void XYCoordInStation(double sta, ref double X, ref double Y)
        {
            CoordGeomType type = CoordGeomType.line;
            int iSec = 0;

            NumberFormatInfo provider = FormatProvider();

            for (int i = 0; i < aligment.CoordGeom.Line.Count; i++)
            {
                Line line = aligment.CoordGeom.Line[i];

                double staStart = Convert.ToDouble(line.StaStart);
                double staEnd = staStart + Convert.ToDouble(line.Length, provider);

                if (sta >= staStart && sta <= staEnd)
                {
                    iSec = i;
                    goto Found;
                }
            }

            for (int i = 0; i < aligment.CoordGeom.Curve.Count; i++)
            {
                Curve curve = aligment.CoordGeom.Curve[i];

                double staStart = Convert.ToDouble(curve.StaStart);
                double staEnd = staStart + Convert.ToDouble(curve.Length);

                if (sta >= staStart && sta <= staEnd)
                {
                    type = CoordGeomType.curve;
                    iSec = i;
                    goto Found;
                }
            }

            for (int i = 0; i < aligment.CoordGeom.Spiral.Count; i++)
            {
                Spiral spiral = aligment.CoordGeom.Spiral[i];

                double staStart = Convert.ToDouble(spiral.StaStart);
                double staEnd = staStart + Convert.ToDouble(spiral.Length);

                if (sta >= staStart && sta <= staEnd)
                {
                    type = CoordGeomType.spiral;
                    iSec = i;
                    goto Found;
                }
            }

        Found:

            switch (type)
            {
                case CoordGeomType.line:
                    XYCoordinatesLineInStation(sta, aligment.CoordGeom.Line[iSec], ref X, ref Y);
                    break;
                case CoordGeomType.curve:
                    break;
                case CoordGeomType.spiral:
                    break;
                default:
                    break;
            }
        }

        private static NumberFormatInfo FormatProvider()
        {
            // Create a NumberFormatInfo object and set some of its properties.
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            return provider;
        }

        void XYCoordinatesLineInStation(double sta, Line line, ref double X, ref double Y)
        {
            NumberFormatInfo provider = FormatProvider();

            double k = (sta - Convert.ToDouble(line.StaStart, provider)) / Convert.ToDouble(line.Length, provider);

            
            

            //double xIni = Convert.ToDouble(line.Start[0], provider);
            //double yIni = Convert.ToDouble(start.PntRef[1], provider);

            //double xFin = Convert.ToDouble(line.End.PntRef[0], provider);
            //double yFin = Convert.ToDouble(line.End.PntRef[1], provider);

            //X = xIni + k * (xFin - xIni);
            //Y = yIni + k * (yFin - yIni);
        }
    }
}
