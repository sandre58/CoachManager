using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class PlayerFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static PlayerDto Get(PlayerModel item, CrudStatus crudStatus)
        {
            if (item == null) return null;

            return new PlayerDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                AddressId = item.AddressId,
                Birthdate = item.Birthdate,
                Gender = item.Gender,
                LicenseNumber = item.LicenseNumber,
                Photo = item.Photo,
                PlaceOfBirth = item.PlaceOfBirth,
                Laterality = item.Laterality,
                CategoryId = item.CategoryId,
                CountryId = item.CountryId,
                Height = item.Height,
                Weight = item.Weight,
                ShoesSize = item.ShoesSize,
                Size = item.Size,
                Description = item.Description,
                Address = item.Address,
                PostalCode = item.PostalCode,
                City = item.City,
                Emails = item.Emails.Select(ContactFactory.GetContact<EmailDto>),
                Phones = item.Phones.Select(ContactFactory.GetContact<PhoneDto>),
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static PlayerModel Get(PlayerDto dto)
        {
            if (dto == null) return null;

            return new PlayerModel
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                AddressId = dto.AddressId,
                Address = dto.Address,
                PostalCode = dto.PostalCode,
                City = dto.City,
                Birthdate = dto.Birthdate,
                Gender = dto.Gender,
                LicenseNumber = dto.LicenseNumber,
                Photo = dto.Photo,
                PlaceOfBirth = dto.PlaceOfBirth,
                Laterality = dto.Laterality,
                CategoryId = dto.CategoryId,
                Category = CategoryFactory.Get(dto.Category),
                CountryId = dto.CountryId,
                Country = CountryFactory.Get(dto.Country),
                Height = dto.Height,
                Weight = dto.Weight,
                ShoesSize = dto.ShoesSize,
                Size = dto.Size,
                Emails = dto.Emails != null ? dto.Emails.Select(ContactFactory.GetContact<EmailModel>).ToList().ToItemsObservableCollection() : new ItemsObservableCollection<EmailModel>(),
                Phones = dto.Phones != null ?dto.Phones.Select(ContactFactory.GetContact<PhoneModel>).ToList().ToItemsObservableCollection() : new ItemsObservableCollection<PhoneModel>(),
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}