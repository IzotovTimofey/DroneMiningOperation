using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamAssigner : MonoBehaviour
{
    [Serializable]
    private class Team
    {
        private bool _isFree = true;
        public Color Color;

        public bool IsFree => _isFree;

        public void SetStatus(bool status)
        {
            _isFree = status;
        }
    }
    [SerializeField] private List<Team> _teamsList;

    public Color GetTeamColor()
    {
        var team = _teamsList.Where(tag => tag.IsFree).First();
        team.SetStatus(false);
        return team.Color;
    }
}
