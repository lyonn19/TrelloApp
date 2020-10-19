
using System;
using System.IO;
using TrelloApp.Utils;
using TrelloApp.ViewModels;
using TrelloApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAddAttachment : ContentPage
    {
        public ViewAddAttachment()
        {
            InitializeComponent();
        }



        private async void BtnTakePhoto_Clicked(object sender, System.EventArgs e)
        {

            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    ViewModelLocator.Instance.Resolve<TrelloViewModel>().ImagenToUpload = StreamHelpers.ReadFully(stream);
                    resultImage.Source = ImageSource.FromStream(() => new MemoryStream(ViewModelLocator.Instance.Resolve<TrelloViewModel>().ImagenToUpload));
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Warning", "Feature not supported", "Accept");
            }
            
        }

        private async void BtnPickImagen_Clicked(object sender, System.EventArgs e)
        {

            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                                       
                    ViewModelLocator.Instance.Resolve<TrelloViewModel>().ImagenToUpload = StreamHelpers.ReadFully(stream);
                    resultImage.Source = ImageSource.FromStream(() => new MemoryStream(ViewModelLocator.Instance.Resolve<TrelloViewModel>().ImagenToUpload));
                }

            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Warning", "Feature not supported", "Accept");
            }
                        
        }

        private void UploadImagen_Clicked(object sender, EventArgs e)
        {
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().AttachmentToCardCommand.Execute(null);
        }
    }

}