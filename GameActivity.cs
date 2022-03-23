using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangedMan
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        private ImageView imgHanged;
        private TextView txtWord;
        private EditText edtLetter;
        private Button btnCheck;
        private string word;
        private TextView txtGuesses;
        private Button btnStartGame;
        private HangMan hanged;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            InitializeViews();
            
            
        }

        private void InitializeViews()
        {
            imgHanged = FindViewById<ImageView>(Resource.Id.imgHanged);
            txtWord = FindViewById<TextView>(Resource.Id.txtWord);
            edtLetter = FindViewById<EditText>(Resource.Id.edtLetter);
            btnCheck = FindViewById<Button>(Resource.Id.btnCheck);
            word = Intent.GetStringExtra("WORD");
            txtGuesses = FindViewById<TextView>(Resource.Id.txtGuesses);
            btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);
            hanged = new HangMan(word);


            btnCheck.Click += BtnCheck_Click;
            btnStartGame.Click += BtnStartGame_Click;
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            txtWord.Text = "";
            txtGuesses.Text = "Guesses:0";
            imgHanged.SetImageResource(Resource.Drawable.hanger);
            Intent intent = new Intent();
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (!hanged.HasLives())
            {
                txtGuesses.Text = "YOU LOST!";
                btnCheck.Clickable = false;
            }
            else
            {
                if (edtLetter.Text == "")
                    Toast.MakeText(this, "Please Enter A Letter", ToastLength.Short);
                else
                {
                    hanged.CheckLetter(char.Parse(edtLetter.Text));
                    switch (hanged.GetGuesses()) 
                    {
                        case 1:
                            imgHanged.SetImageResource(Resource.Drawable.hanged1);
                            break;
                        case 2:
                            imgHanged.SetImageResource(Resource.Drawable.hanged2);
                            break;
                        case 3:
                            imgHanged.SetImageResource(Resource.Drawable.hanged3);
                            break;
                        case 4:
                            imgHanged.SetImageResource(Resource.Drawable.hanged4);
                            break;
                        case 5:
                            imgHanged.SetImageResource(Resource.Drawable.hanged5);
                            break;
                        case 6:
                            imgHanged.SetImageResource(Resource.Drawable.hangedfinal);
                            break;
                    }
                    txtWord.Text = hanged.GetCustomWord();
                    txtGuesses.Text = "Guesses:" + hanged.GetGuesses().ToString();
                    if (hanged.EndGame())
                    {
                        txtGuesses.Text = "YOU WON!";
                        btnCheck.Clickable = false;
                    }
                }
            }
        }
    }
}