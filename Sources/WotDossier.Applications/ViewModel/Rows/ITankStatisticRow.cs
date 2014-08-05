﻿using System.Collections.Generic;
using WotDossier.Domain.Interfaces;
using WotDossier.Domain.Tank;

namespace WotDossier.Applications.ViewModel.Rows
{
    public interface ITankStatisticRow : IStatisticBase, IStatisticExtended, IStatisticRatings, IRandomBattlesAchievements, ITeamBattlesAchievements, IHistoricalBattlesAchievements, IClanBattlesAchievements, IFortAchievements, ITankFilterable
    {
        TankIcon Icon { get; set; }

        TankDescription Description { get; set; }
        
        IEnumerable<ITankStatisticRow> GetAll();

        void SetPreviousStatistic(ITankStatisticRow model);

        ITankStatisticRow GetPreviousStatistic();
    }
}