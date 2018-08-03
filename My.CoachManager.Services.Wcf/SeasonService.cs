﻿using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos.Seasons;
using My.CoachManager.Application.Services.Seasons;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class SeasonService : ISeasonService
    {
        /// <summary>
        /// Get Seasons list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SeasonDto> GetList()
        {
            return ServiceLocator.Current.GetInstance<ISeasonAppService>().GetList();
        }

        /// <summary>
        /// Get Season.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SeasonDto GetById(int id)
        {
            return ServiceLocator.Current.GetInstance<ISeasonAppService>().GetById(id);
        }

        /// <summary>
        /// Create Season.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public SeasonDto CreateOrUpdate(SeasonDto categoryDto)
        {
            return ServiceLocator.Current.GetInstance<ISeasonAppService>().CreateOrUpdate(categoryDto);
        }

        /// <summary>
        /// Remove Season.
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public void Remove(SeasonDto categoryDto)
        {
            ServiceLocator.Current.GetInstance<ISeasonAppService>().Remove(categoryDto);
        }

        /// <summary>
        /// Update Seasons Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            ServiceLocator.Current.GetInstance<ISeasonAppService>().UpdateOrders(entities);
        }
    }
}