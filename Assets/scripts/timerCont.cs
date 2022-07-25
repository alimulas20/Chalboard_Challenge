using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timerCont : MonoBehaviour
{ 
    public int CountdownTime;
    public int TimerTime;
    public Text CountDisplay;
    public Text TimerDisplay;
    public Text ScoreText;
    public Text Header;
    public Text MultText;
    public Text Op1;
    public Text Op2;
    public Image Check;
    public Image Cross;
    public Image Circle1;
    public Image Circle2;
    public Image Circle3;
    public Image Circle4;
    public Image Circle5;
    int Score;
    int mult=1; //for storing lvl
    int multCount=0;//for storing where in lvl
    int choice = 0;
    bool choiceBool = true;
    int result1;
    int result2;

    void Start()
    {
    StartCoroutine(countDown());
        MultText.text = "x1";
    }
    public IEnumerator TimerStart()
    {
        StartCoroutine(NumberGenerator());
        while (TimerTime > 0)
        {
            TimerDisplay.text = TimerTime.ToString();
            Header.text = "Determine which side is larger";
            yield return new WaitForSeconds(1f);
            TimerTime--;
        }
        TimerDisplay.gameObject.SetActive(false);
    
    }

    public IEnumerator countDown()
    {
        while (CountdownTime > 0)
        {
            CountDisplay.text = CountdownTime.ToString();
            yield return new WaitForSeconds(1f);
            CountdownTime--;
        }
        CountDisplay.gameObject.SetActive(false);
        yield return TimerStart();
    }
   public IEnumerator NumberGenerator()
    {
        while (mult == 1)
        {   result1= Random.Range(1, 30);
            Op1.text = result1.ToString();
            result2 = Random.Range(1, 30);
            Op2.text = result2.ToString();
            yield return new WaitWhile(()=>(choice==0||!choiceBool));
            yield return new WaitForSeconds(1f);
            Check.gameObject.SetActive(false);
            Cross.gameObject.SetActive(false);
            choice = 0;
        }
        while(mult == 2||mult==3||mult==4)
        {
            int number1 = Random.Range(1, 31);
            int number2 = Random.Range(1, 31);
            int number3 = Random.Range(1, 10);
            int number4 = Random.Range(1, 10);
            int operation = Random.Range(0, 3);
            int operation2 = Random.Range(0, 3);
            int sideSelect;
            if(mult == 2)
                sideSelect = Random.Range(0, 2);
            else if(mult==3)//katsayý 3 ise iki tarafýnda iþlem olma ihtimali oluþuyor.
                sideSelect = Random.Range(0, 3);
            else {//katsayý 4 ise sadece iki tarafta iþlem oluyor
                sideSelect = 2;
            }
            //tam bölünsün diye
            if (operation == 3)
            {
                if (number1 > number3)
                    number1 -= number1 % number3;
                else
                    number1 = number3;
            }//çýkarma sonucunun pozitif olmasýný saðlýyor
            else if (operation == 2 && number1 < number3)
            {
                number1 = number1 + number3;
            }
            //iþlem yapýlýyor sonuç bulunuyor ayný zamanda text oluþturuluyor
            //iki faklý ihtimal var biri sadece sayý olmasý diðeri iþlem olmasý
            //biri sayý ise diðeri kesin iþlem
            if (sideSelect == 0)
            {
                result1 = operationBasic(number1, number3, operation);
                Op1.text= operationBasicText(number1, number3, operation);
                result2 = number2;
                Op2.text = number2.ToString();
            }
            else if(sideSelect == 1)
            {
                result1 = number2;
                Op1.text = number2.ToString();
                result2 = operationBasic(number1, number3, operation);
                Op2.text = operationBasicText(number1, number3, operation);
            }
            else
            {
                if (operation2 == 3)
                {
                    if (number2 > number4)
                        number2 -= number2 % number4;
                    else
                        number2 = number4;
                }else if (operation2 == 2&&number2<number4)
                {
                    number2 = number2 + number4;
                }
                
                result1 = operationBasic(number1, number3, operation);
                Op1.text = operationBasicText(number1, number3, operation);
                result2 = operationBasic(number2, number4, operation2);
                Op2.text = operationBasicText(number2, number4, operation2);
            }
            yield return new WaitWhile(() => (choice == 0 || !choiceBool));
            yield return new WaitForSeconds(1f);
            Check.gameObject.SetActive(false);
            Cross.gameObject.SetActive(false);
            choice = 0;
        }
        while (mult>=5)
        {
            int number1 = Random.Range(1, 31);
            int number2 = Random.Range(1, 31);
            int number3 = Random.Range(1, 10);
            int number4 = Random.Range(1, 10);
            int number5 = Random.Range(1, 10);
            int number6 = Random.Range(1, 10);
            int operation1 = Random.Range(0, 3);
            int operation2 = Random.Range(0, 3);
            int operation3 = Random.Range(0, 3);
            int operation4 = Random.Range(0, 3);
            int sideSelect;
            //5-6lvl bir taraf parantezli iþlem olabilir üç ihtimal var parantezli iþlem saðda , parantezli iþlem solda
            //parantezli iþlem yok
            //7 parantezli iþlem kesin iki ihtimal var parantez saðda veya solda
            //8-9 üç ihtimal var iki tarafta da parantez olabiliyor 7ye ek olarak
            //10 görünmüyor fakat ben hep parantezli iþlem olarak tasarladým
            //side select: 0 parantezli iþlem yok ,1 parantez sol, 2 parantez sað, 3 iki taraf parantezli iþlem
            if (mult == 5 || mult == 6)
                sideSelect = Random.Range(0, 3);
            else if (mult == 7)
                sideSelect = Random.Range(1, 3);//parantezli iþlem ok ihtimali ortadan kalktý
            else if (mult == 8 || mult == 9)
                sideSelect = Random.Range(1, 4);//iki tarafta da parantezli iþlem olma olasýlýðý eklendi.
            else
                sideSelect = 3;
            if (sideSelect == 0)
            {
                result1 = operationBasic(number1, number3, operation1);
                Op1.text = operationBasicText(number1, number3, operation1);
                result2 = operationBasic(number2, number4, operation2);
                Op2.text = operationBasicText(number2, number4, operation2);
            }
            else if (sideSelect ==1)
            {
                result1=operationComplex(number1, number3, number5, operation1, operation3);
                Op1.text = operationComplexText(number1, number3, number5, operation1, operation3);
                result2 = operationBasic(number2, number4, operation2);
                Op2.text = operationBasicText(number2, number4, operation2);


            }
            else if (sideSelect == 2)
            {
                result1 = operationBasic(number1, number3, operation1);
                Op1.text = operationBasicText(number1, number3, operation1);
                result2 = operationComplex(number2, number4, number6, operation2, operation4);
                Op2.text = operationComplexText(number2, number4, number6, operation2, operation4);
            }
            else 
            {
                result1 = operationComplex(number1, number3, number5, operation1, operation3);
                Op1.text = operationComplexText(number1, number3, number5, operation1, operation3);
                result2 = operationComplex(number2, number4, number6, operation2, operation4);
                Op2.text = operationComplexText(number2, number4, number6, operation2, operation4);
            }

            yield return new WaitWhile(() => (choice == 0 || !choiceBool));
            yield return new WaitForSeconds(1f);
            Check.gameObject.SetActive(false);
            Cross.gameObject.SetActive(false);
            choice = 0;



        }
        

    }
    int operationBasic(int number,int number2,int operation)
    {
        if (operation == 0)
        {
            return number + number2;
        } else if (operation == 1)
        {
            return number * number2;
        }else if (operation == 2)
        {
            return number - number2;
        }
        else
        {
            return number / number2;
        }
    }
    string operationBasicText(int number, int number2, int operation)
    {
        if (operation == 0)
        {
            return number +" + "+ number2;
        }
        else if (operation == 1)
        {
            return number + " * " + number2;
        }
        else if (operation == 2)
        {
            return number + " - " + number2;
        }
        else
        {
            return number + " / " + number2;
        }
    }
    int operationComplex(int number,int number2,int number3,int operation1,int operation2)
    {
        return operationBasic(operationBasic(number, number2, operation1), number3, operation2);
    }
    string operationComplexText(int number, int number2, int number3, int operation1, int operation2)
    {
        string inBracket = "("+operationBasicText(number, number2, operation1)+")";
        string temp = operationBasicText(1, number3, operation2);
        return inBracket + temp.Substring(1, temp.Length - 1);

    }
    void comparator()
    {
        if (choice == 1)
        {
            if (result1 > result2)
            {
                Check.gameObject.SetActive(true);
                choiceBool = true;
                Score += mult * 100;
                ScoreText.text = Score.ToString();
                multCount++;
            }
            else
            {
                Cross.gameObject.SetActive(true);
                choiceBool = true;
                if (multCount != 0)
                    multCount--;
            }

        }
        else if(choice==2)
        {
            if (result1 < result2)
            {
                Check.gameObject.SetActive(true);
                choiceBool = true;
                Score += mult * 100;
                ScoreText.text = Score.ToString();
                multCount++;
            }
            else
            {
                Cross.gameObject.SetActive(true);
                choiceBool = true;
                if (multCount != 0)
                    multCount--;
            }
        }
        else
        {
            if (result1 == result2)
            {
                Check.gameObject.SetActive(true);
                choiceBool = true;
                Score += mult * 100;
                ScoreText.text = Score.ToString();
                multCount++;

            }
            else
            {
                Cross.gameObject.SetActive(true);
                choiceBool = true;
                if(multCount!=0)
                    multCount--;
            }
        }
        if (multCount == 5)
        {
            mult += 1;
            MultText.text = "x" + mult;
        }
        StartCoroutine(MultHandler());
    }
    public IEnumerator MultHandler()
    {
        if (multCount == 1) {
            Circle1.color = new Color32(2, 58, 26, 255);
            Circle2.color = new Color32(2, 58, 26, 110);
            Circle3.color = new Color32(2, 58, 26, 110);
            Circle4.color = new Color32(2, 58, 26, 110);
            Circle5.color = new Color32(2, 58, 26, 110);
        }
            
        else if(multCount == 2)
        {
            Circle1.color = new Color32(2, 58, 26, 255);
            Circle2.color = new Color32(2, 58, 26, 255);
            Circle3.color = new Color32(2, 58, 26, 110);
            Circle4.color = new Color32(2, 58, 26, 110);
            Circle5.color = new Color32(2, 58, 26, 110);
        }
     
        else if(multCount == 3)
        {
            Circle1.color = new Color32(2, 58, 26, 255);
            Circle2.color = new Color32(2, 58, 26, 255);
            Circle3.color = new Color32(2, 58, 26, 255);
            Circle4.color = new Color32(2, 58, 26, 110);
            Circle5.color = new Color32(2, 58, 26, 110);
        }
        else if (multCount == 4)
        {
            Circle1.color = new Color32(2, 58, 26, 255);
            Circle2.color = new Color32(2, 58, 26, 255);
            Circle3.color = new Color32(2, 58, 26, 255);
            Circle4.color = new Color32(2, 58, 26, 255);
            Circle5.color = new Color32(2, 58, 26, 110);
        }
        else if (multCount == 5)
        {
            Circle1.color = new Color32(2, 58, 26, 255);
            Circle2.color = new Color32(2, 58, 26, 255);
            Circle3.color = new Color32(2, 58, 26, 255);
            Circle4.color = new Color32(2, 58, 26, 255);
            Circle5.color = new Color32(2, 58, 26, 255);
            yield return new WaitForSeconds(1f);
            multCount = 0;
            StartCoroutine(MultHandler());
        }
        else
        {
            Circle1.color = new Color32(2, 58, 26, 110);
            Circle2.color = new Color32(2, 58, 26, 110);
            Circle3.color = new Color32(2, 58, 26, 110);
            Circle4.color = new Color32(2, 58, 26, 110);
            Circle5.color = new Color32(2, 58, 26, 110);
        }


    }
    public void selector1()
    {
        if (choice == 0)
        {
            choice = 1;
            choiceBool = false;
            comparator();
        }
    }
    public void selector2()
    {
        if (choice == 0)
        {
            choice = 2;
            choiceBool = false;
            comparator();
        }
    }
    public void selector3()
    {
        if (choice == 0)
        {
            choice = 3;
            choiceBool = false;
            comparator();
        }
    }
}