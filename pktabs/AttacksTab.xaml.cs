using System;
using System.Windows.Input;
using PKHeX.Core;

using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.DataSource.Extensions;
using static PKHeXMAUI.MainPage;


namespace PKHeXMAUI;


public partial class AttacksTab : ContentPage
{
    public bool SkipEvent = false;
	public AttacksTab()
	{
		InitializeComponent();
        move1ppups.ItemsSource = new List<int>() { 0, 1, 2, 3 };
        move2ppups.ItemsSource = new List<int>() { 0, 1, 2, 3 };
        move3ppups.ItemsSource = new List<int>() { 0, 1, 2, 3 };
        move4ppups.ItemsSource = new List<int>() { 0, 1, 2, 3 };
        eggsprite.IsVisible = pk.IsEgg;
        if(pk.Species != 0)
            applyattackinfo(pk);
        
    }
    public static List<ComboItem> movlist = new();
	public void applyattackinfo(PKM pkm)
	{
        SkipEvent = true;
        eggsprite.IsVisible = pkm.IsEgg;
        if (pkm.HeldItem > 0)
        {
            itemsprite.Source = itemspriteurl;
            itemsprite.IsVisible = true;
        }
        else
            itemsprite.IsVisible=false;
        if (pkm.IsShiny)
            shinysparklessprite.IsVisible = true;
        else
            shinysparklessprite.IsVisible = false;
        attackpic.Source = spriteurl;
        movlist = new();
       foreach(var mov in datasourcefiltered.Moves)
        {
            LegalMoveInfo p = new();
            p.ReloadMoves(new LegalityAnalysis(pkm));
            if (p.CanLearn((ushort)mov.Value))
            {
                movlist.Add(mov);
            }
        }
        move1.ItemsSource = movlist;
        move2.ItemsSource = movlist;
        move3.ItemsSource = movlist;
        move4.ItemsSource = movlist;
        rmove1.ItemsSource = movlist;
        rmove2.ItemsSource = movlist;
        rmove3.ItemsSource = movlist;
        rmove4.ItemsSource = movlist;
    
        move1.SelectedItem = movlist.Where(z=>z.Value== pkm.Move1).FirstOrDefault();
        move2.SelectedItem = movlist.Where(z => z.Value == pkm.Move2).FirstOrDefault();
        move3.SelectedItem = movlist.Where(z => z.Value == pkm.Move3).FirstOrDefault();
        move4.SelectedItem = movlist.Where(z => z.Value == pkm.Move4).FirstOrDefault();
        rmove1.SelectedItem = movlist.Where(z => z.Value == pkm.RelearnMove1).FirstOrDefault();
        rmove2.SelectedItem = movlist.Where(z => z.Value == pkm.RelearnMove2).FirstOrDefault();
        rmove3.SelectedItem = movlist.Where(z => z.Value == pkm.RelearnMove3).FirstOrDefault();
        rmove4.SelectedItem = movlist.Where(z => z.Value == pkm.RelearnMove4).FirstOrDefault();
        move1pp.Text = pkm.GetMovePP(pkm.Move1, pkm.Move1_PPUps).ToString();
        move2pp.Text = pkm.GetMovePP(pkm.Move2, pkm.Move2_PPUps).ToString();
        move3pp.Text = pkm.GetMovePP(pkm.Move3, pkm.Move3_PPUps).ToString();
        move4pp.Text = pkm.GetMovePP(pkm.Move4, pkm.Move4_PPUps).ToString();
        move1ppups.SelectedIndex = pkm.Move1_PPUps;
        move2ppups.SelectedIndex = pkm.Move2_PPUps;
        move3ppups.SelectedIndex = pkm.Move3_PPUps;
        move4ppups.SelectedIndex = pkm.Move4_PPUps;
        if (pk is IMoveShop8Mastery)
            moveshopbutton.IsVisible = true;
        if (pk is PA8 pa8)
        {
            AlphaMasteredLabel.IsVisible = true;
            AlphaMasteredPicker.IsVisible = true;
            AlphaMasteredPicker.ItemsSource = movlist;
            AlphaMasteredPicker.DisplayMemberPath = "Text";
            AlphaMasteredPicker.SelectedItem = movlist.Where(z => z.Value == pa8.AlphaMove).FirstOrDefault();
        }
        SkipEvent = false;
    }

    private void applymove1(object sender, EventArgs e)
    {
        if(!SkipEvent)
            pk.Move1 = move1.SelectedIndex >= 0? (ushort)((ComboItem)move1.SelectedItem).Value:pk.Move1;
    }
    private void applymove2(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move2 = move2.SelectedIndex >= 0? (ushort)((ComboItem)move2.SelectedItem).Value:pk.Move2;
    }
    private void applymove3(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move3 = move3.SelectedIndex >= 0 ? (ushort)((ComboItem)move3.SelectedItem).Value : pk.Move3;
    }
    private void applymove4(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move4 = move4.SelectedIndex >= 0 ? (ushort)((ComboItem)move4.SelectedItem).Value : pk.Move4;
    }
    private void applyrmove1(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.RelearnMove1 = rmove1.SelectedIndex >= 0 ? (ushort)((ComboItem)rmove1.SelectedItem).Value : pk.RelearnMove1;
    }
    private void applyrmove2(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.RelearnMove2 = rmove2.SelectedIndex >= 0 ? (ushort)((ComboItem)rmove2.SelectedItem).Value : pk.RelearnMove2;
    }
    private void applyrmove3(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.RelearnMove3 = rmove3.SelectedIndex >= 0 ? (ushort)((ComboItem)rmove3.SelectedItem).Value : pk.RelearnMove3;
    }
    private void applyrmove4(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.RelearnMove4 = rmove4.SelectedIndex >= 0 ? (ushort)((ComboItem)rmove4.SelectedItem).Value : pk.RelearnMove4;
    }

    private void setsuggmoves(object sender, EventArgs e)
    {
        SkipEvent = true;
        var m = new ushort[4];
        pk.GetMoveSet(m,true);
        pk.SetMoves(m);
        pk.HealPP();
        move1.SelectedItem = (Move)pk.Move1;
        move2.SelectedItem = (Move)pk.Move2;
        move3.SelectedItem = (Move)pk.Move3;
        move4.SelectedItem = (Move)pk.Move4;
        rmove1.SelectedItem = (Move)pk.RelearnMove1;
        rmove2.SelectedItem = (Move)pk.RelearnMove2;
        rmove3.SelectedItem = (Move)pk.RelearnMove3;
        rmove4.SelectedItem = (Move)pk.RelearnMove4;
        SkipEvent = false;
    }

    private void applymove1ppups(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move1_PPUps = move1ppups.SelectedIndex;
    }
    private void applymove2ppups(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move2_PPUps = move2ppups.SelectedIndex;
    }
    private void applymove3ppups(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move3_PPUps = move3ppups.SelectedIndex;
    }
    private void applymove4ppups(object sender, EventArgs e)
    {
        if (!SkipEvent)
            pk.Move4_PPUps = move4ppups.SelectedIndex;
    }

    private void openTReditor(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new TREditor());
    }

    private void refreshmoves(object sender, EventArgs e)
    {
        if (pk.Species != 0)
            applyattackinfo(pk);
    }

    private void openMoveShopEditor(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new MoveShopEditor());
    }

    private void applyAlphaMasteredMove(object sender, EventArgs e)
    {
        if (!SkipEvent)
        {
            var selectedmove = (ComboItem)AlphaMasteredPicker.SelectedItem;
            if (pk is PA8 pa8)
                pa8.AlphaMove = (ushort)selectedmove.Value;
        }
    }

    private void applyPP1(object sender, TextChangedEventArgs e)
    {
        
            pk.Move1_PP = int.Parse(move1pp.Text);
    }
    private void applyPP2(object sender, TextChangedEventArgs e)
    {
        
            pk.Move2_PP = int.Parse(move2pp.Text);
    }
    private void applyPP3(object sender, TextChangedEventArgs e)
    {
        
            pk.Move3_PP = int.Parse(move3pp.Text);
    }
    private void applyPP4(object sender, TextChangedEventArgs e)
    {
        
            pk.Move4_PP = int.Parse(move4pp.Text);
    }

    private void ChangeComboBoxFontColor(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        SfComboBox box = (SfComboBox)sender;
        if (box.IsDropDownOpen)
            box.TextColor = Colors.Black;
        else
            box.TextColor = Colors.White;
    }
}