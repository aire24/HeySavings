using System;
using System.Threading.Tasks;
using HeySavings.Models;
using HeySavings.Views;
using Xamarin.Forms;

namespace HeySavings.ViewModels
{
    public class OtpPageViewModel : BaseViewModel
    {
        string OTP;

        public OtpPageViewModel(string otp, string emailid)
        {
            OTP = otp;
            EmailId = emailid;
        }

        bool _isloading;
        public bool isLoading
        {
            get { return _isloading; }
            set {
                SetProperty(ref _isloading, value);
            }
        }

        string _emailid;
        public string EmailId {
            get { return _emailid; }
            set {
                SetProperty(ref _emailid, value);
            }
        }


        private string _hideEnrtyText;
        public string HideEnrtyText
        {
            get => _hideEnrtyText;
            set {
                SetProperty(ref _hideEnrtyText, value);
                SetOtp(); }
        }

        private void SetOtp()
        {
            if (HideEnrtyText.Length >= 1)
            {
                OTP_FirstLetter = HideEnrtyText.Substring(0, 1);
            }
            else
            {
                OTP_FirstLetter = null;
            }
            if (HideEnrtyText.Length >= 2)
            {
                OTP_SecondLetter = HideEnrtyText.Substring(1, 1);
            }
            else
            {
                OTP_SecondLetter = null;
            }
            if (HideEnrtyText.Length >= 3)
            {
                OTP_ThirdLetter = HideEnrtyText.Substring(2, 1);
            }
            else
            {
                OTP_ThirdLetter = null;
            }
            if (HideEnrtyText.Length >= 4)
            {
                OTP_FourthLetter = HideEnrtyText.Substring(3, 1);
            }
            else
            {
                OTP_FourthLetter = null;
            }
        }

        private string _otp_FirstLetter;
        public string OTP_FirstLetter
        {
            get => _otp_FirstLetter;
            set => SetProperty(ref _otp_FirstLetter, value);
        }

        private string _otp_SecondLetter;
        public string OTP_SecondLetter
        {
            get => _otp_SecondLetter;
            set => SetProperty(ref _otp_SecondLetter, value);
        }

        private string _otp_ThirdLetter;
        public string OTP_ThirdLetter
        {
            get => _otp_ThirdLetter;
            set => SetProperty(ref _otp_ThirdLetter, value);
        }

        private string _otp_FourthLetter;
        public string OTP_FourthLetter
        {
            get => _otp_FourthLetter;
            set => SetProperty(ref _otp_FourthLetter, value);
        }


        public Command VerifyOtp =>
          new Command(() =>
          {
              if (string.IsNullOrEmpty(HideEnrtyText))
              {

                  ShowSnackbar("Enter otp");
                  return;
              }
              try
              {

                  if (HideEnrtyText == OTP)
                  {
                      App.Current.MainPage = new NavigationPage(new LoginPage());
                      ShowSnackbar("Email Verified Successfully!");

                  }
                  else { ShowSnackbar("Invalid otp");
                      HideEnrtyText = "";
                  }
              }
              catch (Exception ex)
              {

              }
              finally
              {
              }
          });


        public Command Resend => new Command(async() =>
        {
            isLoading = true;
            await Task.Run(() =>
            {
                try
                {
                    string otp = SendEmail.SendMail(EmailId);
                    if (string.IsNullOrEmpty(otp))
                    {
                        ShowSnackbar("Error Occured");
                    }
                    else {
                        ShowSnackbar("Otp Resent Successfully!");

                        OTP = otp;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    isLoading = false;
                }
            });
            

        });
    }
}
