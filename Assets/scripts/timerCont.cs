using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class timerCont : MonoBehaviour
{ 
    public int CountdownTime;
    public int TimerTime;
    public Text CountDisplay;
    public Text TimerDisplay;
    public Text TimeAdder;
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
    public AudioSource MultSound;
    public AudioSource CountDownSound;

    public static int Score/*=21500*/;//skorboard ve topScorers ekranlarýnýn kontrolü için;


    public static int mult=1; //for storing lvl
    public static int QuesCount;
    public static int wrongCount;
    int multCount=0;//for storing where in lvl
    int choice = 0;
    bool choiceBool = true;
    int result1;
    int result2;
    bool wrongAns;
    bool countDownBool = false;
    void Start()
    {
        StartCoroutine(countDown());
        MultText.text = "x1";
        mult = 1;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            selector1();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            selector2();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            selector3();
        }

    }
    public IEnumerator TimerStart()
    {
        StartCoroutine(NumberGenerator());
        while (TimerTime > 0)
        {
            
            if(TimerTime%60>9)
                TimerDisplay.text = TimerTime / 60 + ":" + TimerTime % 60;
            else
            {
                TimerDisplay.text = TimerTime / 60 + ":0" + TimerTime % 60;
            }
            Header.text = "Determine which side is larger";
            yield return new WaitForSeconds(1f);
            TimerTime--;
            if (!countDownBool && TimerTime <= 5)
            {
                CountDownSound.Play();
                countDownBool = true;
            }
        }
        TimerDisplay.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            if (wrongAns)
            {
                yield return new WaitForSeconds(0.5f);
                TimeAdder.text = "";
            }

            choice = 0;
        }
        while(mult == 2||mult==3||mult==4)
        {
            int number1 = Random.Range(1, 31);
            int number2 = Random.Range(1, 31);
            int number3 = Random.Range(1, 10);
            int number4 = Random.Range(1, 10);
            int operation = Random.Range(0, 4);
            int operation2 = Random.Range(0, 4);
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
                if (operation == 3)
                {
                    if (number1 > number3)
                        number1 += number3 - (number1 % number3);
                    else
                        number1 = number3;
                }
                else if (operation == 2 && number1 < number3)
                {
                    number1 = number1 + number3;
                }
                if (operation2 == 3)
                {
                    if (number2 > number4)
                        number2 += number4-(number2 % number4);
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
            if (wrongAns)
            {
                yield return new WaitForSeconds(0.5f);
                TimeAdder.text = "";
            }

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
            int operation1 = Random.Range(0, 4);
            int operation2 = Random.Range(0, 4);
            int operation3 = Random.Range(0, 4);
            int operation4 = Random.Range(0, 4);
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

           
            if (operation1 == 3)
            {
                if (number1 > number3)
                    number1 += number3-(number1 % number3);
                else
                    number1 = number3;
            }//çýkarma sonucunun pozitif olmasýný saðlýyor
            else if (operation1 == 2 && number1 < number3)
            {
                number1 = number1 + number3;
            }
            if (operation2 == 3)
            {
                if (number2 > number4)
                    number2 += number4 - (number2 % number4);
                else
                    number1 = number3;
            }//çýkarma sonucunun pozitif olmasýný saðlýyor
            else if (operation2 == 2 && number2 < number4)
            {
                number2 = number2 + number4;
            }
            if (operation3 == 3)//parantezin dýþýndaki iþlem bölme olduðunda parantez içi toplama oluyor
            {//tam bölünmeyi saðlamak için ikinci sayýya tam bölünme için gereken sayý ekleniyor 
                operation1 = 0;
                number3 += number5 - ((number1 + number3) % number5);
            }
            if (operation4 == 3)
            {
                operation2 = 0;
                number4 += number6 - ((number2 + number4) % number6);
            }
            if (operation3 == 2)//parantezli iþlem sonrasýnda çýkarmanýn negatif olma ihtimali düzenleniyor
            {
                if(number5> operationBasic(number1, number3, operation1))
                number5 = operationBasic(number1, number3, operation1);
            }
            if (operation4 == 2)
            {
                if(number6> operationBasic(number2, number4, operation2))
                number6 = operationBasic(number2, number4, operation2);
            }
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
            if (wrongAns) {
                yield return new WaitForSeconds(0.5f);
                TimeAdder.text = "";
            }
            
            
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
            return number + " x " + number2;
        }
        else if (operation == 2)
        {
            return number + " - " + number2;
        }
        else
        {
            return number + " ÷ " + number2;
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
        QuesCount++;
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
               WrongAnswer();
                
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
            WrongAnswer();
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
                WrongAnswer();
            }
        }
        if (multCount == 5)
        {
            mult += 1;
            MultText.text = "x" + mult;
            MultSound.Play();
        }
        StartCoroutine(MultHandler());
        StartCoroutine(crossHandler());
    }
    void WrongAnswer()
    {
        wrongCount++;
        Cross.gameObject.SetActive(true);
        TimerTime -= 3;
        TimeAdder.text = "-3 seconds";
        Op1.text = result1.ToString();
        Op2.text = result2.ToString();
        Op1.color = Color.yellow;
        Op2.color = Color.yellow;
        choiceBool = true;
        wrongAns = true;
        
        if (multCount != 0)
            multCount--;
    }
    public IEnumerator crossHandler()
    {
        yield return new WaitForSeconds(0.5f);
        Check.gameObject.SetActive(false);
        Cross.gameObject.SetActive(false);
        Op1.color = new Color32(27,68,212,255);
        Op2.color = new Color32(27, 68, 212, 255);
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
            TimerTime+=10;
            TimeAdder.text = "+10 seconds";
            yield return new WaitForSeconds(1f);
            TimeAdder.text = "";
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