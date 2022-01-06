using System;

namespace ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;
        private const string exceptionMsg = "{0} cannot be zero or negative.";
        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get
            {
                return this.length;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(exceptionMsg, nameof(this.Length)));
                }

                this.length = value;
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(exceptionMsg, nameof(this.Width)));
                }

                this.width = value;
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(exceptionMsg, nameof(this.Height)));
                }

                this.height = value;
            }
        }

        public void CalculateSurfaceArea()
        {
            double result = 2 * (this.Length * this.Width) + 2 * (this.Length * this.Height) + 2 * (this.Width * this.Length);

            Console.WriteLine($"Surface Area - {result:F2}");
        }

        public void CalculateLateralSurfaceArea()
        {
            double result = 2 * (this.Length * this.Height) + 2 * (this.Width * this.Length);

            Console.WriteLine($"Lateral Surface Area - {result:F2}");
        }

        public void CalculateVolume()
        {
            double result = this.Length * this.Width * this.Height;

            Console.WriteLine($"Volume - {result:F2}");
        }
    }
}
