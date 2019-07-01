using System.Linq;
using Silo.Util;

namespace Silo.Memory
{
    public class Counter : Component
    {
        public Counter() : base(13, 9)
        {
        }

        public override void DoUpdate()
        {
            if (Current[0]) //Reset
            {
                UpdateOutput(0, false);
                UpdateOutputRange(1, 8, false);
                return;
            }

            if (Current[4] != Last[4])
            {
                if (Current[4])
                {
                    if (Current[1]) //Load or count
                    {
                        UpdateOutput(0, false);
                        UpdateOutputRange(1, Current.Skip(5).ToArray());
                    }
                    else
                    {
                        if (Current[3])
                        {
                            var currentVal = Enumerable.Range(1, 8).Select(GetPortState).ToArray().ConvertToByte();
                            if (Current[2]) //Up or down
                            {
                                if (currentVal != byte.MaxValue)
                                {
                                    currentVal++;
                                }
                            }
                            else
                            {
                                //Down
                                currentVal--;
                            }
                            
                            UpdateOutput(0, currentVal == byte.MaxValue);

                            var vals = currentVal.ConvertToBoolArray();
                            UpdateOutputRange(1, vals);
                        }
                    }
                }
            }
        }
    }
}