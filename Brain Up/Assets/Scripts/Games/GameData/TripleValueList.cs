/*
    Author: Ghercioglo "Romeon0" Roman
 */
using System;
using UnityEngine;

namespace Assets.Scripts.Games.Gamedata.TripleValueList
{
    [Serializable]
    public class TripleValueListRow
    {
        public string item1;
        public string item2;
        public string item3;

        public TripleValueListRow(string i1, string i2, string i3)
        {
            item1 = (string)i1.Clone();
            item2 = (string)i2.Clone();
            item3 = (string)i3.Clone();
        }

        public TripleValueListRow Clone()
        {
            return new TripleValueListRow(item1, item2, item3);
        }
    }

    [CreateAssetMenu(fileName = "TripleValueList", menuName = "ScriptableObjects/TripleValueList", order = 1)]
    public class TripleValueList : ScriptableObject
    {
        public TripleValueListRow[] rows;

        public void SetRows(TripleValueListRow[] rows)
        {
            this.rows = new TripleValueListRow[rows.Length];
            int counter = 0;
            foreach (TripleValueListRow row in rows)
            {
                this.rows[counter++] = row.Clone();
            }
           // this.rows = (TripleValueListRow[])rows.Clone();
        }

        public TripleValueList Clone()
        {
            TripleValueList data = ScriptableObject.CreateInstance<TripleValueList>();
            data.SetRows(rows);
            return data;
        }
    }
}
