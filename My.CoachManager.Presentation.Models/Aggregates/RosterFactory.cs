using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Models.Aggregates
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class RosterFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static RosterDto Get(RosterModel item, CrudStatus crudStatus)
        {
            if (item == null) return null;

            return new RosterDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                Name = item.Name,
                SeasonId = item.SeasonId,
                CategoryId = item.CategoryId
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static RosterModel Get(RosterDto dto)
        {
            if (dto == null) return null;

            var result = new RosterModel
            {
                Id = dto.Id,
                Name = dto.Name,
                SeasonId = dto.SeasonId,
                CategoryId = dto.CategoryId,
                Category = CategoryFactory.Get(dto.Category),
                Season = SeasonFactory.Get(dto.Season),
                Squads = dto.Squads != null ? dto.Squads.Select(SquadFactory.Get).ToObservableCollection() : new ObservableCollection<SquadModel>(),
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
            result.ResetModified();

            return result;
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static RosterPlayerModel Get(RosterPlayerDto dto)
        {
            if (dto == null) return null;

            var result = new RosterPlayerModel
            {
                Id = dto.Id,
                IsMutation = dto.IsMutation,
                LicenseState = dto.LicenseState,
                Number = dto.Number,
                FirstName = dto.Player.FirstName,
                LastName = dto.Player.LastName,
                AddressId = dto.Player.AddressId,
                Address = dto.Player.Address,
                PostalCode = dto.Player.PostalCode,
                City = dto.Player.City,
                Birthdate = dto.Player.Birthdate,
                FromDate = dto.Player.FromDate,
                Gender = dto.Player.Gender,
                LicenseNumber = dto.Player.LicenseNumber,
                Photo = dto.Player.Photo,
                PlaceOfBirth = dto.Player.PlaceOfBirth,
                Laterality = dto.Player.Laterality,
                CategoryId = dto.Player.CategoryId,
                Category = CategoryFactory.Get(dto.Player.Category),
                CountryId = dto.Player.CountryId,
                Country = CountryFactory.Get(dto.Player.Country),
                Height = dto.Player.Height,
                Weight = dto.Player.Weight,
                ShoesSize = dto.Player.ShoesSize,
                Size = dto.Player.Size,
                Emails = dto.Player.Emails != null ? dto.Player.Emails.Select(ContactFactory.GetContact<EmailModel>).ToList().ToItemsObservableCollection() : new ObservableItemsCollection<EmailModel>(),
                Phones = dto.Player.Phones != null ? dto.Player.Phones.Select(ContactFactory.GetContact<PhoneModel>).ToList().ToItemsObservableCollection() : new ObservableItemsCollection<PhoneModel>(),
                Positions = dto.Player.Positions != null ? dto.Player.Positions.Select(PlayerFactory.GetPosition).ToList().ToObservableCollection() : new ObservableCollection<PlayerPositionModel>(),
                Injuries = dto.Player.Injuries != null ? dto.Player.Injuries.Select(InjuryFactory.Get).ToList().ToObservableCollection() : new ObservableCollection<InjuryModel>(),
                PlayerId = dto.PlayerId,
                SquadId = dto.SquadId,
                Squad = SquadFactory.Get(dto.Squad),
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
            result.ResetModified();

            return result;
        }

        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static RosterPlayerDto Get(RosterPlayerModel item, CrudStatus crudStatus)
        {
            if (item == null) return null;

            var player = new RosterPlayerDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                PlayerId = item.PlayerId,
                IsMutation = item.IsMutation,
                LicenseState = item.LicenseState,
                Number = item.Number,
                SquadId = item.SquadId,
                Player = PlayerFactory.Get(item, crudStatus)
            };
            player.Player.Id = item.PlayerId;

            return player;
        }
    }
}