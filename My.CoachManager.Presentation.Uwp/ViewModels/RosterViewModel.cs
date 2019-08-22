﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Uwp.Core.ViewModels;
using HttpHelper = My.CoachManager.Presentation.Uwp.Helpers.HttpHelper;

namespace My.CoachManager.Presentation.Uwp.ViewModels
{
    public class RosterViewModel : ScreenViewModel
    {
        public ObservableCollection<CategoryModel> Players { get; set; }

        protected override async Task LoadDataCoreAsync()
        {
            var result = await HttpHelper.GetDataAsync<IEnumerable<CategoryDto>>("api/category");

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Players = result.Select(CategoryFactory.Get).ToObservableCollection();
            });
            
        }
    }
}