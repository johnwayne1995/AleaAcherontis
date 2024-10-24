using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    public class NormalCardsConfig : ScriptableObject
    {
        public List<NormalCard> NormalCards = new List<NormalCard>();
    }

    [Serializable]
    public class NormalCard
    {
        public string cardName;
        public string cardImgPath;
        public float cardPoint;
    }
}
