
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
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        private async void BtnPickImagen_Clicked(object sender, System.EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });


            var stream2 = await result.OpenReadAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }

            ViewModelLocator.Instance.Resolve<TrelloViewModel>().ImagenToUpload = StreamHelpers.ReadFully(stream2);

        }

        private void UploadImagen_Clicked(object sender, EventArgs e)
        {
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().AttachmentToCardCommand.Execute(null);
        }
    }

}