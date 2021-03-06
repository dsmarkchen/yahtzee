﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using NUnit.Framework;

public class Dice
{
    Random rand;
    public Dice() { 
        rand = new Random();

    }
    public Dice(Random ra) {
        rand = ra;
    }
    public int Roll() {
        return rand.Next(1,7);
    }
}

public class Yahtzee
{
    Random rand;

    public const uint DICE_TOTAL = 5;
    public const byte SCORE_T_CHANCE = 0; 
    public const byte SCORE_T_YAHTZEE = 1; 
    public const byte SCORE_T_ONES = 2; 
    public const byte SCORE_T_PAIR = 3; 
    public const byte SCORE_T_TWOPAIR = 4; 
    public const byte SCORE_T_THREE = 5; 
    public const byte SCORE_T_FOUR = 6;
    public const byte SCORE_T_SMALL_STRAIGHT = 7;
    public const byte SCORE_T_LARGE_STRAIGHT = 8;
    public const byte SCORE_T_FULLHOUSE = 9;

    private Dice[] dice = null;
    public Yahtzee() {
        rand = new Random();
        dice = new Dice[DICE_TOTAL];
        for(int i=0;i<DICE_TOTAL;i++) {
            dice[i] = new Dice(rand);
        }
    }
    public  void play(out int[] scores){
        scores = new int[DICE_TOTAL];
        for(int i=0;i<DICE_TOTAL;i++)
            scores[i] = dice[i].Roll();
    }

    private int yahtzee_score(int[] scores) 
    {
        if((scores[0] == scores[1]) &&  
                (scores[0] == scores[2])  &&
                (scores[0] == scores[3])  &&
                (scores[0] == scores[4])  )  {
            return 50;
        }
        return 0;
    }
    private int ones_score(int[] scores, int x0)
    {
        int j=0;
        for(int i=0;i<DICE_TOTAL;i++) 
            if(x0==scores[i]) j++;
        return j*x0;
    }
    private int pair_score(int[] scores)
    {
        int[] cnt;
        build_counter(scores, out cnt);
        int r = 0;
        for(int j=0;j<6;j++) {
            if(cnt[j] == 2)  r=(j+1)*2;
        }
        return r;
    }

    /// k pair counter
    /// r scored number
    private void update_pair(int[] cnt, int j, ref int k, ref int r) 
    {
        if(cnt[j] != 2) return;

        k++;  
        r += (j+1)*2;
    }
    private int twopair_score(int[] scores)
    {

        int[] cnt;
        build_counter(scores, out cnt);


        int r = 0;
        int k = 0;
        for(int j=0;j<6;j++) {
            update_pair(cnt, j, ref k, ref r);
        }
        if(k!=2) return 0;
        return r;
    }
    private int three_score(int[] scores)
    {
        int[] cnt;
        build_counter(scores, out cnt);

        int r = 0;
        for(int j=0;j<6;j++) {
            if(cnt[j] == 3)  r=(j+1)*3;
        }
        return r;
    }
    private int four_score(int[] scores)
    {
        int[] cnt;

        build_counter(scores, out cnt);
        int r = 0;
        for(int j=0;j<6;j++) {
            if(cnt[j] == 4)  r=(j+1)*4;
        }
        return r;
    }

    private void build_counter(int[] scores, out int[] cnt) {
        cnt = new int [6] {0,0,0,0,0,0};

        for(int j=0;j<6;j++) {
            for(int i=0;i<5;i++) {
                if((j+1)==scores[i]) cnt[j]++;
            }

        }
    }

    private bool check_small_straight(int[] cnt) {
        if((cnt[0]==cnt[1]) && (cnt[0] == cnt[2]) && 
            (cnt[0]==cnt[3]) && (cnt[0]==cnt[4]) && 
                (cnt[0]==1) && cnt[5] == 0) 
            return true;
        return false;

    } 
    private int small_score(int[] scores) 
    {

        int[] cnt;
        build_counter(scores, out cnt);
        if(check_small_straight(cnt)) return 15;

        return 0;
    }       
    private int large_score(int[] scores) 
    {

        int[] cnt;
        build_counter(scores, out cnt);
        if(check_large_straight(cnt)) return 20;

        return 0;
    }       
 
 
    private bool check_large_straight(int [] cnt) {

        bool r = false;
        if((cnt[5]==cnt[1]) && (cnt[5] == cnt[2]) && (cnt[5]==cnt[3]) &&
                (cnt[5]==cnt[4]) && (cnt[5]==1) && cnt[0] == 0) r = true;

        return r;

    }
    public int score(int[] scores, byte score_type = SCORE_T_CHANCE, int x0 = 1) 
    {
        int yahtzees = yahtzee_score(scores);
        if(score_type == SCORE_T_YAHTZEE) return yahtzees;

        int ones = ones_score(scores, x0);
        if(score_type == SCORE_T_ONES) return ones;

        int pair = pair_score(scores);
        if(score_type == SCORE_T_PAIR) return pair;

        int pairs = twopair_score(scores);
        if(score_type == SCORE_T_TWOPAIR) return pairs;

        int three_sum = three_score(scores);
        if(score_type == SCORE_T_THREE) return three_sum;

        int four_sum = four_score(scores);
        if(score_type == SCORE_T_FOUR) return four_sum;

        int fullhouse = 0;
        if(pair != 0 && three_sum !=0) {
            fullhouse = pair + three_sum;
        }     
        if(score_type == SCORE_T_FULLHOUSE) return fullhouse; 


        int small_straight_sum = small_score(scores);
        if(score_type == SCORE_T_SMALL_STRAIGHT) return small_straight_sum;

        int large_straight_sum = large_score(scores);
        if(score_type == SCORE_T_LARGE_STRAIGHT) return large_straight_sum;
        int sums = 0;
        for(int i=0;i<DICE_TOTAL;i++)
            sums += scores[i];
        return sums;

    }
}


