/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCombination : MonoBehaviour {
    public static Hand getBestHand(Hand hand)
    {
        if (hand.Count() < 5)
        {
            hand.Clear();
            return hand;
        }
        if (isRoyalFlush(hand))
            return getRoyalFlush(hand);
        if (isStraightFlush(hand))
            return getStraightFlush(hand);
        if (isFourOfAKind(hand))
            return getFourOfAKind(hand);
        if (isFullHouse(hand))
            return getFullHouse(hand);
        if (isFlush(hand))
            return getFlush(hand);
        if (isStraight(hand))
            return getStraight(hand);
        if (isThreeOfAKind(hand))
            return getThreeOfAKind(hand);
        if (isTwoPair(hand))
            return getTwoPair(hand);
        if (isOnePair(hand))
            return getOnePair(hand);
        return getHighCard(hand);
    }
}

*/