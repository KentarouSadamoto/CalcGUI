using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CalcGUI
{
    public partial class Form1 : Form
    {
        //変数定義
        List<string> valueList = new List<string>(); //入力された式の保存リスト
        List<int> indexList = new List<int>(); //演算子がvalueListのどの位置にあるかを示すポインタのリスト
        List<int> muldivList = new List<int>(); //×,÷がvalueListのどの位置にあるかを示すポインタのリスト
        List<double> resultList = new List<double>(); //各計算結果を一時的に保存するリスト
        StringBuilder sb = new StringBuilder(); //入力された数値、演算子を一時的に保存するStringBuilder
        bool op_flag = false; //入力された文字列が演算子かどうかを判定するフラグ
        double sum = 0; //加算、減算の合計
        double mul_sum = 0;//乗算、除算の合計
        double All_Result=0; //最終的な計算結果

        //数値入力のプログラム
        public Form1()
        {
            InitializeComponent();
        }

        private void botton1_Click(object sender, EventArgs e)
        {
            sb.Append("1");
            view_Clicked();
        }

        private void botton2_Click(object sender, EventArgs e)
        {
            sb.Append("2");
            view_Clicked();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sb.Append("3");
            view_Clicked();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sb.Append("4");
            view_Clicked();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sb.Append("5");
            view_Clicked();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sb.Append("6");
            view_Clicked();
        }

        private void botton7_Click(object sender, EventArgs e)
        {
            sb.Append("7");
            view_Clicked();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sb.Append("8");
            view_Clicked();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sb.Append("9");
            view_Clicked();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sb.Append("0");
            view_Clicked();
        }

        //小数点入力ボタン        

        private void decimal_point_button_Click(object sender, EventArgs e)
        {
            if (sb.ToString() != ".")
            {
                sb.Append(".");
                view_Clicked();
            }
            else
            {
                Output.Text = ".";
            }
        }

        //演算子ボタン

        private void add_Click(object sender, EventArgs e)
        {
            operation();
            sb.Append("+");
            view_Clicked();
        }

        private void sub_button_Click(object sender, EventArgs e)
        {
            operation();
            sb.Append("-");
            view_Clicked();
        }

        private void mul_button_Click(object sender, EventArgs e)
        {
            operation();
            sb.Append("×");
            view_Clicked();
        }

        private void div_button_Click(object sender, EventArgs e)
        {
            operation();
            sb.Append("÷");
            view_Clicked();
        }

        //＝ボタン

        private void equall_botton_Click(object sender, EventArgs e)
        {
            valueList.Add(sb.ToString());
            result();
        }

        //クリアボタン

        private void clear_botton_Click(object sender, EventArgs e)
        {
            sb.Clear();
            valueList.Clear();
            indexList.Clear();
            muldivList.Clear();
            resultList.Clear();
            sum = 0;
            op_flag = false;
            Output.Text = sb.ToString();
            All_Result = 0;
        }

        //数値,演算子の表示、値の代入
        private void view_Clicked()
        {
            if(All_Result != 0)
            {
                sum = All_Result;
                All_Result = 0;
            }
            Output.Text = sb.ToString();
            if (op_flag == true)
            {
                indexList.Add(valueList.Count()); //演算子がvalueListのどこにあるのかを保存
                if (valueList.Count() != 0)//1文字目に演算子が入力されたらvalueListに保存しない。
                {
                    valueList.Add(sb.ToString());
                }
                
                if (sb.ToString() == "×")  //×,÷の場合、valueListのどこにあるのかを保存
                {
                    muldivList.Add(valueList.Count()-1);
                }
                else if (sb.ToString() == "÷")
                {
                    muldivList.Add(valueList.Count()-1);
                }
                sb.Clear();
                op_flag = false;
            }
        }

        private void operation()
        {
            

                valueList.Add(sb.ToString()); //直前の数値をvalueListに保存
                sb.Clear();
                op_flag = true;
            //}        
        }
        private void result()
        {
            if (muldivList.Count() != 0)//乗算、除算があれば先に実行
            {
                for (int i = 0; i < muldivList.Count(); i++)           
                {
                    if(mul_sum== 0) //2数の計算ならば
                    {
                        if (sum == 0) //前回の計算結果がなければ、通常通り計算する。
                        {
                            if (valueList[muldivList[i]] == "×")
                            {
                                mul_sum=Convert.ToDouble(valueList[muldivList[i] - 1]) * Convert.ToDouble(valueList[muldivList[i] + 1]);
                                valueList[muldivList[i]] = "+";
                                valueList[muldivList[i] - 1] = "0";
                                valueList[muldivList[i] + 1] = "0";
                            }
                            else if (valueList[muldivList[i]] == "÷")
                            {
                                if (valueList[muldivList[i] + 1] == "0")
                                {
                                    MessageBox.Show("0による除算は実行できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    mul_sum=Convert.ToDouble(valueList[muldivList[i] - 1]) / Convert.ToDouble(valueList[muldivList[i] + 1]);
                                    valueList[muldivList[i]] = "+";
                                    valueList[muldivList[i] - 1] = "0";
                                    valueList[muldivList[i] + 1] = "0";
                                }
                            }
                        }
                        else  //前回の計算結果があれば、その数を被乗数、被除数とする。
                        {
                            if (valueList[muldivList[i]] == "×")
                            {
                                mul_sum = sum * Convert.ToDouble(valueList[muldivList[i] + 1]);
                                sum = 0;
                                valueList[muldivList[i]] = "+";
                                valueList[muldivList[i] - 1] = "0";
                                valueList[muldivList[i] + 1] = "0";
                            }
                            else if (valueList[muldivList[i]] == "÷")
                            {
                                if (valueList[muldivList[i] + 1] == "0")
                                {
                                    MessageBox.Show("0による除算は実行できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    mul_sum = sum / Convert.ToDouble(valueList[muldivList[i] + 1]);
                                    sum = 0;
                                    valueList[muldivList[i]] = "+";
                                    valueList[muldivList[i] - 1] = "0";
                                    valueList[muldivList[i] + 1] = "0";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (valueList[muldivList[i]] == "×")
                        {
                            mul_sum = mul_sum * Convert.ToDouble(valueList[muldivList[i] + 1]);
                            valueList[muldivList[i]] = "+";
                            valueList[muldivList[i] - 1] = "0";
                            valueList[muldivList[i] + 1] = "0";
                        }
                        else if (valueList[muldivList[i]] == "÷")
                        {
                            if (valueList[muldivList[i] + 1] == "0")
                            {
                                MessageBox.Show("0による除算は実行できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                mul_sum = mul_sum / Convert.ToDouble(valueList[muldivList[i] + 1]);
                                valueList[muldivList[i]] = "+";
                                valueList[muldivList[i] - 1] = "0";
                                valueList[muldivList[i] + 1] = "0";
                            }
                        }
                    }
                    if(i == muldivList.Count()-1) //乗算、除算を全て実行した後に計算結果を格納する
                    {
                        resultList.Add(mul_sum);
                    }
                }
            }
                for (int i = 0; i < indexList.Count(); i++) //加算、減算の実行
                {
                    if (sum == 0)//1回目は演算子の左右を計算
                    {
                        if (valueList[indexList[i]] == "+") //加算を実行
                        {
                            sum = Convert.ToDouble(valueList[indexList[i] - 1]) + Convert.ToDouble(valueList[indexList[i] + 1]);
                        }
                        else //減算を実行
                        {
                            sum = Convert.ToDouble(valueList[indexList[i] - 1]) - Convert.ToDouble(valueList[indexList[i] + 1]);
                        }
                    }
                    else //2回目以降は合計に演算子の1つ奥のインデックスを計算
                    {
                        if (valueList[indexList[i]] == "+") //加算を実行
                        {
                            sum = sum + Convert.ToDouble(valueList[indexList[i] + 1]);
                        }
                        else //減算を実行
                        {
                            sum = sum - Convert.ToDouble(valueList[indexList[i] + 1]);
                        }
                    }
                    if (i == indexList.Count() - 1) //ループの最後に加算、減算の結果を計算結果のリストに格納
                    {
                        resultList.Add(sum);
                    }
                }
            foreach(double d in resultList) //加算、減算、乗算、除算の結果を合計する。
            {
                All_Result += d;
            }
            sb.Clear();                    //変数の初期化
            valueList.Clear();
            indexList.Clear();
            muldivList.Clear();
            resultList.Clear();
            sum = 0;
            mul_sum = 0;
            op_flag = false;
            Output.Text = All_Result.ToString();
        }
    }
}
