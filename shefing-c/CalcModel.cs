using System;

namespace shefing_c.Entities
{
    /// <summary>
    /// The calculation model object, converted from the posted JSON value.
    /// We make it Comparable in order to use it as a key for the cash map
    /// @author Zvi Lifshitz
    /// </summary>
    public class CalcModel : IComparable<CalcModel>
    {
        private string op;
        private int left, right;

        public virtual string Operator
        {
            get
            {
                return op;
            }
            set
            {
                this.op = value;
            }
        }


        public virtual int Left
        {
            get
            {
                return left;
            }
            set
            {
                this.left = value;
            }
        }


        public virtual int Right
        {
            get
            {
                return right;
            }
            set
            {
                this.right = value;
            }
        }


        public virtual int CompareTo(CalcModel o)
        {
            return this.Equals(o) ? 0 : 1;
        }

        /// <summary>
        /// Calculate a hash code for a field. If it is null return zero. </summary>
        /// <param name="val"> the field </param>
        /// <returns> the value of the object's hash code or zero if the object is null </returns>
        private int hash(string val)
        {
            return val == null ? 0 : val.GetHashCode();
        }

        /// <summary>
        /// We generate the object hash code by xoring all fields' hash codes </summary>
        /// <returns>  </returns>
        public override int GetHashCode()
        {
            return hash(left.ToString()) ^ hash(op) ^ hash(right.ToString());
        }

        /// <summary>
        /// Objects are equal if the 3 fields are equal </summary>
        /// <param name="obj">   object to compare to </param>
        /// <returns>      true if and only if all fields are equal </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CalcModel))
            {
                return false;
            }
            CalcModel o = (CalcModel)obj;
            return Object.Equals(left, o.left) && Object.Equals(op, o.op) && Object.Equals(right, o.right);
        }
    }
}