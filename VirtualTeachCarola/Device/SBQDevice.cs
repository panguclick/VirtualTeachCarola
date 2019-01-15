using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VirtualTeachCarola.Base;

namespace VirtualTeachCarola
{
    class SBQDevice : Device
    {
        public void FlashFlashCommand(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FSCommandEvent e)
        {
            SetTipValue("");

            if (e.command == "power")
            {
                if (e.args == "off" || e.args == "acc")
                {
                }
                else if (BbValue.BaseValue != "" && RbValue.BaseValue != "")
                {
                    UpdateRBValue();
                }
                else
                {
                    SetTipValue("");
                }
            }
            else if (e.command == "BB")
            {
                if (e.args != "")
                {
                    BbValue.BaseValue = e.args;
                    UpdateRBValue();
                }
                else
                {
                    BbValue.Reset();
                    SetTipValue("");
                }
            }
            else if (e.command == "RB")
            {
                if (e.args != "")
                {
                    RbValue.BaseValue = e.args;
                    UpdateRBValue();
                }
                else
                {
                    RbValue.Reset();
                    SetTipValue("");
                }
            }
            else if (e.command == "ZL")
            {
                Record();
            }

            ShowValue();

            Console.WriteLine("e.command = " + e.command);
            Console.WriteLine("e.args = " + e.args);
        }

        private void ShowValue()
        {
            if(FlashContrl != null)
            {
                FlashContrl.SetVariable("SBQ", TipValue);
            }
        }

        private bool ExcuteSQL(RBData rbData, RBData bbData)
        {
            bool res = false;
            try
            {
                if (rbData.BaseValue == "" || bbData.BaseValue == "")
                {
                    TipValue = "";
                    return false;
                }

                string sql = "CkPoint1 = '" + rbData.BaseValue
                    + "' AND CkPoint2 = '" + bbData.BaseValue
                    + "' AND Accorrun = " + Manager.GetInstance().Car.Power()
                    + " AND IsLine = " + Manager.GetInstance().Car.IsLine;
                DataRow[] rowData = Manager.GetInstance().BytDataTable.Select(sql);

                if (rowData.Length == 0)
                {
                    bool isYoumen = false;
                    Manager.ExcuteSQLNeedTwoPoint(bbData, rbData, 1, Manager.GetInstance().CkvalueDataTbale, ref isYoumen);

                    CanYouMen = isYoumen;
                    float value = rbData.MinValue - bbData.MinValue;
                    if (CanYouMen == true)
                    {
                        value = rbData.MinValue + Manager.GetInstance().Car.Speed * (rbData.MaxValue - rbData.MinValue) / 100;
                    }

                    TipValue = "Z" + value.ToString();
                    return res;
                }

                TipValue = (string)rowData[0]["BH"];

                res = true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return res;
        }

        private void UpdateRBValue()
        {
            ExcuteSQL(RbValue, BbValue);
        }


        private void SetTipValue(string value)
        {
            TipValue = value;
        }

        private void Record()
        {

        }
    }
}
