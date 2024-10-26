using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Config
{
    public class CardCaseConfig : ScriptableObject
    {
        public List<CardCase> CardCases = new List<CardCase>();
    }

    [Serializable]
    public class CardCase
    {
        public CaseEnum caseEnum;
        public int damageValue;
        public int magnification;
    }
}
