﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Bluetooth;
using App11Athletics.ViewModels.Timers;
using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class TabataFeatureView : ContentPage
    {
        public TabataFeatureView()
        {
            InitializeComponent();
            TabataFeatureViewModel = new TabataFeatureViewModel();
            BindingContext = TabataFeatureViewModel;

            if (labelAnimateStatus.Text != null)
                WorkTimePrevious = labelAnimateStatus.Text;
            WorkTimeText = WorkTimePrevious;
            tabataOptions.TranslationY = 0;
            labelAnimateStatus.Opacity = 0;
        }

        #region Overrides of Page

        protected override bool OnBackButtonPressed()
        {
            if (TabataOptionsUp)
            {
                if (!tabataOptions.Valid)
                    return true;
                tabataOptions.TranslateTo(0, Height, 350U, Easing.CubicOut);
                TabataOptionsUp = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }

        #endregion

        public TabataFeatureViewModel TabataFeatureViewModel;
        public static bool WorkTimeBool { get; set; }
        public static string WorkTimeText { get; set; } = string.Empty;
        public static string WorkTimePrevious { get; set; } = string.Empty;
        public bool TabataOptionsUp { get; set; }



        private async void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            var label = (Label)sender;
            if (label != null)
            {
                label.AnchorY = 0;

            }
            if (labelAnimateStatus?.Text != null)
                WorkTimeText = labelAnimateStatus.Text;
            if (WorkTimeText == WorkTimePrevious)
                return;

            if (WorkTimeText == "Work Time!")
            {
                statusAnimation.AnimateWorkTimeTask(1);
            }
            else
            {
                statusAnimation.AnimateWorkTimeTask(0);
            }

            if (label != null)
            {
                Debug.WriteLine("animating");
                FadeLabel(label);
            }

            WorkTimePrevious = WorkTimeText;

        }

        private static async Task FadeLabel(Label label)
        {
            label.FadeTo(1, 350U);
            await Task.Delay(2000);
            await label.FadeTo(0, 500U);
        }

        private void LabelAnimateStatus_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {



        }

        private void TabataOptions_OnClicked(object sender, EventArgs e)
        {
            TabataFeatureViewModel.TotalRounds = Convert.ToInt32(tabataOptions.TotalRounds);
            TabataFeatureViewModel.TotalRoundTimeTimeSpan = TimeSpan.FromMinutes(tabataOptions.TimeOnMinutes) + TimeSpan.FromSeconds(tabataOptions.TimeOnSeconds);
            TabataFeatureViewModel.TotalTimeOffTimeSpan = TimeSpan.FromMinutes(tabataOptions.TimeOffMinutes) + TimeSpan.FromSeconds(tabataOptions.TimeOffSeconds);
            tabataOptions.TranslateTo(0, Height, 350U, Easing.CubicIn);
            TabataOptionsUp = false;
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            if (TabataOptionsUp || TabataFeatureViewModel.TimerRunning)
                return;
            TabataOptionsUp = true;
            await tabataOptions.TranslateTo(0, 0, 350U, Easing.CubicIn);
            TabataFeatureViewModel.ResetCommandMethod();
        }
    }
}
