using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(SeasonPlayerMetadata))]
    public class SeasonPlayer : ForeignEntity
    {
        public SeasonPlayer()
        {
            Mutation = PlayerConstants.DefaultMutation;
            LicenseState = PlayerConstants.DefaultLicenseState;
        }

        public int SeasonId { get; set; }

        public Season Season { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public LicenseState LicenseState { get; set; }

        public bool Mutation { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }
    }
}