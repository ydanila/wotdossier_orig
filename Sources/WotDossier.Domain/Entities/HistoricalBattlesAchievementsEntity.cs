using System.Collections.Generic;
using WotDossier.Domain.Interfaces;

namespace WotDossier.Domain.Entities
{
    /// <summary>
    /// Object representation for table 'HistoricalBattlesAchievements'.
    /// </summary>
    public class HistoricalBattlesAchievementsEntity : EntityBase, IHistoricalBattlesAchievements, IRevised
    {
        public virtual int GuardsMan { get; set; }

        public virtual int MakerOfHistory { get; set; }

        public virtual int BothSidesWins { get; set; }

        public virtual int WeakVehiclesWins { get; set; }

        #region Collections

        private IList<HistoricalBattlesAchievementsEntity> _historicalBattlesAchievementsEntities;

        /// <summary>
        ///     Gets/Sets the <see cref="RandomBattlesStatisticEntity" /> collection.
        /// </summary>
        public virtual IList<HistoricalBattlesAchievementsEntity> HistoricalBattlesAchievementsEntities
        {
            get { return _historicalBattlesAchievementsEntities ?? (_historicalBattlesAchievementsEntities = new List<HistoricalBattlesAchievementsEntity>()); }
            set { _historicalBattlesAchievementsEntities = value; }
        }

        #endregion Collections
    }
}