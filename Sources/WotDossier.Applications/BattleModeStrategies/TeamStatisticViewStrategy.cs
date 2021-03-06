using System;
using System.Collections.Generic;
using WotDossier.Applications.Logic.Adapter;
using WotDossier.Applications.ViewModel.Rows;
using WotDossier.Applications.ViewModel.Statistic;
using WotDossier.Dal;
using WotDossier.Domain.Entities;
using WotDossier.Domain.Server;
using WotDossier.Domain.Tank;

namespace WotDossier.Applications.BattleModeStrategies
{
    public class TeamStatisticViewStrategy : StatisticViewStrategyBase
    {
        /// <summary>
        /// Predicate to get tank statistic
        /// </summary>
        public override Func<TankJson, StatisticJson> Predicate
        {
            get { return tank => tank.A7x7 ?? new StatisticJson(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamStatisticViewStrategy"/> class.
        /// </summary>
        /// <param name="dossierRepository">The dossier repository.</param>
        public TeamStatisticViewStrategy(DossierRepository dossierRepository) : base(dossierRepository)
        {
        }

        /// <summary>
        /// Gets the player statistic.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="tanks">The tanks.</param>
        /// <param name="playerData">The player data.</param>
        /// <returns></returns>
        public override PlayerStatisticViewModel GetPlayerStatistic(PlayerEntity player, List<TankJson> tanks, Player playerData = null)
        {
            return GetPlayerStatistic<TeamBattlesStatisticEntity>(player, tanks, playerData);
        }

        /// <summary>
        /// To the tank statistic row.
        /// </summary>
        /// <param name="currentStatistic">The current statistic.</param>
        /// <param name="prevStatisticViewModels">The previous statistic view models.</param>
        /// <returns></returns>
        protected override ITankStatisticRow ToTankStatisticRow(TankJson currentStatistic, List<StatisticSlice> prevStatisticViewModels)
        {
            TeamBattlesTankStatisticRowViewModel model = new TeamBattlesTankStatisticRowViewModel(currentStatistic, prevStatisticViewModels);
            return model;
        }

        /// <summary>
        /// To the view model.
        /// </summary>
        /// <param name="currentStatistic">The current statistic.</param>
        /// <returns></returns>
        protected override PlayerStatisticViewModel ToViewModel(StatisticEntity currentStatistic)
        {
            return new TeamBattlesPlayerStatisticViewModel((TeamBattlesStatisticEntity)currentStatistic);
        }

        /// <summary>
        /// To the view model.
        /// </summary>
        /// <param name="currentStatistic">The current statistic.</param>
        /// <param name="oldStatisticEntities">The old statistic entities.</param>
        /// <returns></returns>
        protected override PlayerStatisticViewModel ToViewModel(StatisticEntity currentStatistic, List<StatisticSlice> oldStatisticEntities)
        {
            return new TeamBattlesPlayerStatisticViewModel((TeamBattlesStatisticEntity)currentStatistic, oldStatisticEntities);
        }

        /// <summary>
        /// Updates the tank statistic.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="tanks">The tanks.</param>
        /// <returns></returns>
        public override PlayerEntity UpdateTankStatistic(int playerId, List<TankJson> tanks)
        {
            return UpdateTankStatistic<TankTeamBattleStatisticEntity>(playerId, tanks);
        }

        /// <summary>
        /// Gets the tanks statistic.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns></returns>
        public override List<ITankStatisticRow> GetTanksStatistic(int playerId)
        {
            return GetTanksStatistic<TankTeamBattleStatisticEntity>(playerId);
        }

        /// <summary>
        /// Updates the player statistic.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="tanks">The tanks.</param>
        /// <param name="serverStatistic">The server statistic.</param>
        /// <returns></returns>
        public override PlayerEntity UpdatePlayerStatistic(int playerId, List<TankJson> tanks, Player serverStatistic)
        {
            return DossierRepository.UpdatePlayerStatistic(new TeamBattlesStatAdapter(tanks), serverStatistic, playerId);
        }
    }
}