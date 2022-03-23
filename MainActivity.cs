using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using MODEL;
using System;

namespace HangedMan
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText edtWord;
        private Button btnSend;
        private Words wordList = new Words();
        private Button btnStart;
        private TextView txtWordList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.layout1);
            InitializeViews();
        }

        private void InitializeViews()
        {
            edtWord= FindViewById<EditText>(Resource.Id.edtWord);
            btnSend = FindViewById<Button>(Resource.Id.btnSend);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            txtWordList= FindViewById<TextView>(Resource.Id.txtWordList);

            btnSend.Click += BtnSend_Click;
            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
           if(wordList.GetLength() >= 3)
            {
                Intent intent = new Intent(this,typeof(GameActivity));
                intent.PutExtra("WORD",wordList.GetWord());
                StartActivityForResult(intent, 1);
            }
           else if(wordList.GetLength() < 3)
            {
                Toast.MakeText(this, "Insert atleast 3 words", ToastLength.Short).Show();
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (edtWord.Text.Length <= 2)
                Toast.MakeText(this, "Insert 3 Letter word or more",ToastLength.Short).Show();
            else if(edtWord.Text.Length >2)
            { 
                    wordList.AddWord(edtWord.Text);
                    txtWordList.Text = txtWordList.Text + edtWord.Text + ", ";
                    edtWord.Text = "";
            }    
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode,Result resultCode, Intent data)
        {
            if (requestCode == 1)
            {
                if (resultCode == Result.Ok)
                {
                    Intent intent = new Intent(this, typeof(GameActivity));
                    intent.PutExtra("WORD", wordList.GetWord());
                    StartActivityForResult(intent, 1);
                }
                else
                {
                    wordList = new Words();
                }
            }
        }

    }
}