using WotDossier.Domain.Entities;

namespace WotDossier.Dal.Mappings
{
    /// <summary>
    /// 	Represents base mapping class for entity.
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    public class StatisticClassMapBase<T> : ClassMapBase<T>
        where T : StatisticEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticClassMapBase{T}"/> class.
        /// </summary>
        public StatisticClassMapBase()
        {
            Map(v => v.UId);
            Map(v => v.Updated);
            Map(v => v.Wins);
            Map(v => v.Losses);
            Map(v => v.SurvivedBattles);
            Map(v => v.Xp);
            Map(v => v.BattleAvgXp);
            Map(v => v.MaxXp);
            Map(v => v.Frags);
            Map(v => v.MaxFrags);
            Map(v => v.Spotted);
            Map(v => v.HitsPercents);
            Map(v => v.DamageDealt);
            Map(v => v.DamageTaken);
            Map(v => v.MaxDamage);
            Map(v => v.CapturePoints);
            Map(v => v.DroppedCapturePoints);
            Map(v => v.BattlesCount);
            Map(v => v.AvgLevel);
            Map(v => v.MarkOfMastery);
            Map(v => v.PlayerId).Insert();

            Map(v => v.RBR);
            Map(v => v.WN8Rating);
            Map(v => v.WN8KTTCRating);
            Map(v => v.WN8XVMRating);
            Map(v => v.PerformanceRating);

            Map(v => v.BattlesOnStunningVehicles);
            Map(v => v.StunNum);
            Map(v => v.DamageAssistedStun);

            Map(v => v.Rev);
        }
    }
}