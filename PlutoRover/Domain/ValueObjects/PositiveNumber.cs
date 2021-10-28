using System;

namespace PlutoRover.Domain.ValueObjects
{
    public record PositiveNumber
    {
        private int data;

        public int Data
        {
            get => data;
            init
            {
                Validate(value);
                data = value;
            }
        }

        public PositiveNumber(int number)
        {
            Validate(data);
            Data = number;
        }

        public static void Validate(int number)
        {
            if (number < 0) {
                throw new ArgumentOutOfRangeException(nameof(number), "Can't be a negative number");
            }
        }
    }
}