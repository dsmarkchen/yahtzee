using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using NUnit.Framework;
//using Yahtzee;

[TestFixture]
public class DiceTester 
{
    [Test]
    public void Dice_DiceRoll100Times_GetNumberBetween1to6()
    {
        int r = 0;
        int n;
        Dice o = new Dice();
        for (int i = 0; i < 100; i++)
        {
            n = o.Roll();
            if ((n < 1) || (n > 6)) r++;
#if DEBUG
            Console.WriteLine(n.ToString());
#endif
        }

        Assert.AreEqual(0, r);
    }

    [Test]
    public void Dice_DiceRoll100Times_GetEachNumberAtleastOnce()
    {
        int r1 = 0;
        int r2 = 0;
        int r3 = 0;
        int r4 = 0;
        int r5 = 0;
        int r6 = 0;
        int n;
        Dice o = new Dice();
        for (int i = 0; i < 100; i++)
        {
            n = o.Roll();
            if ((n == 1)) r1++;
            if ((n == 2)) r2++;
            if ((n == 3)) r3++;
            if ((n == 4)) r4++;
            if ((n == 5)) r5++;
            if ((n == 6)) r6++;
        }

        Assert.IsTrue(r1>0);
        Assert.IsTrue(r2>0);
        Assert.IsTrue(r3>0);
        Assert.IsTrue(r4>0);
        Assert.IsTrue(r5>0);
        Assert.IsTrue(r6>0);
    }
}


[TestFixture]
public class YahtzeeTester
{
    [Test]
    public void Yahtzee_PlayTwice_ScoreDifferently()
    {
        int[] scores;
        int[] scores2;
        Yahtzee o = new Yahtzee();
        o.play(out scores);
        o.play(out scores2);
        Assert.AreNotEqual(scores, scores2);
    }

    [Test]
    public void Yahtzee_ChanceScore_1_1_3_3_6_score_14()
    {
        int[] scores = {1,1,3,3,6};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores);
        Assert.AreEqual(14, n);

    }
    [Test]
    public void Yahtzee_ChanceScore_4_5_5_6_1_score_21()
    {
        int[] scores = {4,5,5,6,1};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores);
        Assert.AreEqual(21, n);

    }

    [Test]
    public void Yahtzee_YahtzeeScore_1_1_1_1_1_score_50()
    {
        int[] scores = {1,1,1,1,1};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_YAHTZEE);
        Assert.AreEqual(50, n);

    }
    [Test]
    public void Yahtzee_YahtzeeScore_1_1_1_2_1_score_0()
    {
        int[] scores = {1,1,1,2,1};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_YAHTZEE);
        Assert.AreEqual(0, n);

    }

    [Test]
    public void Yahtzee_OnesScore_1_1_2_4_4_score_8_on_four()
    {
        int[] scores = {1,1,2,4,4};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_ONES, 4);
        Assert.AreEqual(8, n);

    }
    [Test]
    public void Yahtzee_OnesScore_2_3_2_5_1_score_4_on_two()
    {
        int[] scores = {2,3,2,5,1};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_ONES, 2);
        Assert.AreEqual(4, n);

    }
    [Test]
    public void Yahtzee_OnesScore_3_3_3_4_5_score_0_on_one()
    {
        int[] scores = {3,3,3,4,5,};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_ONES, 1);
        Assert.AreEqual(0, n);

    }

    [Test]
    public void Yahtzee_pairscore_3_3_3_4_4_score_8()
    {
        int[] scores = {3,3,3,4,4,};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_PAIR);
        Assert.AreEqual(8, n);
    }
    [Test]
    public void Yahtzee_pairscore_1_1_6_2_6_score_12()
    {
        int[] scores = {1 ,1,6,2,6};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_PAIR);
        Assert.AreEqual(12, n);
    }
    [Test]
    public void Yahtzee_pairscore_3_3_3_4_1_score_0()
    {
        int[] scores = { 3, 3, 3, 4, 1 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_PAIR);
        Assert.AreEqual(0, n);
    }
    [Test]
    public void Yahtzee_pairscore_3_3_3_3_1_score_0()
    {
        int[] scores = { 3, 3, 3, 3, 1 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_PAIR);
        Assert.AreEqual(0, n);
    }
    [Test]
    public void Yahtzee_twopairscore_1_1_2_3_3_score_8()
    {
        int[] scores = {1,1,2,3,3};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_TWOPAIR);
        Assert.AreEqual(8, n);
    }

    [Test]
    public void Yahtzee_twopairscore_1_1_2_3_4_score_0()
    {
        int[] scores = { 1, 1, 2, 3, 4 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_TWOPAIR);
        Assert.AreEqual(0, n);
    }
    [Test]
    public void Yahtzee_twopairscore_1_1_2_2_2_score_0()
    {
        int[] scores = { 1, 1, 2, 2, 2 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_TWOPAIR);
        Assert.AreEqual(0, n);
    }
    [Test]
    public void Yahtzee_threescore_3_3_3_4_5_score_9()
    {
        int[] scores = { 3, 3, 3, 4, 5 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_THREE);
        Assert.AreEqual(9, n);
    }
    [Test]
    public void Yahtzee_threescore_3_3_4_5_6_score_0()
    {
        int[] scores = { 3, 3, 4, 5, 6 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_THREE);
        Assert.AreEqual(0, n);
    }
    [Test]
    public void Yahtzee_threescore_3_3_3_3_1_score_0()
    {
        int[] scores = { 3, 3, 3, 3, 1 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_THREE);
        Assert.AreEqual(0, n);
    }
  
    [Test]
    public void Yahtzee_fourscore_2_2_2_2_5_score_8()
    {
        int[] scores = { 2, 2, 2, 2, 5 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_FOUR);
        Assert.AreEqual(8, n);
    }
    [Test]
    public void Yahtzee_smallstraight_score_15()
    {

        int[] scores = { 1, 2, 3, 4, 5 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_SMALL_STRAIGHT);
        Assert.AreEqual(15, n);
    }
    [Test]
    public void Yahtzee_smallstraight_1_2_2_4_5_score_0()
    {

        int[] scores = { 1, 2, 2, 4, 5 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_SMALL_STRAIGHT);
        Assert.AreEqual(0, n);
    }
    [Test]
    public void Yahtzee_smallstraight_2_3_4_5_1_score_15()
    {

        int[] scores = { 2, 3, 4, 5,1 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_SMALL_STRAIGHT);
        Assert.AreEqual(15, n);
    }
    [Test]
    public void Yahtzee_largestraight_score_20()
    {

        int[] scores = { 2, 3, 4, 5, 6 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_LARGE_STRAIGHT);
        Assert.AreEqual(20, n);
    }
    [Test]
    public void Yahtzee_invalid_largestraight_score_0()
    {

        int[] scores = { 1, 2, 2, 4, 5};
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_LARGE_STRAIGHT);
        Assert.AreEqual(0, n);
    }
     [Test]
    public void Yahtzee_fullhouse_1_1_2_2_2_score_8()
    {

        int[] scores = { 1, 1, 2, 2, 2 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_FULLHOUSE);
        Assert.AreEqual(8, n);
    }
    [Test]
    public void Yahtzee_fullhouse_2_2_3_3_4_score_0()
    {

        int[] scores = { 2, 2, 3, 3, 4 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_FULLHOUSE);
        Assert.AreEqual(0, n);
    }

    [Test]
    public void Yahtzee_fullhouse_4_4_4_4_4_score_0()
    {

        int[] scores = { 4, 4, 4, 4, 4 };
        Yahtzee o = new Yahtzee();
        int n = o.score(scores, Yahtzee.SCORE_T_FULLHOUSE);
        Assert.AreEqual(0, n);
    }
}
