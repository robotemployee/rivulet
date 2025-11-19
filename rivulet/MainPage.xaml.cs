namespace rivulet;

public partial class MainPage : ContentPage
{
    private int _count = 0;

    public MainPage()
    {
        InitializeComponent();
        
        UpdateTime();
        
        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () => {
            UpdateTime();
            return true;
        });
        
        StartCircleAnimation();
    }

    private void OnCounterClicked(object? sender, EventArgs e)
    {
        _count++;

        if (_count == 1)
            CounterBtn.Text = $"Clicked {_count} time";
        else
            CounterBtn.Text = $"Clicked {_count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void UpdateTime() {
        CurrentTimeLabel.Text = DateTime.Now.ToShortTimeString();
    }

    public int GetCount() {
        return _count;
    }

    private void StartCircleAnimation() {
        Animation animation = new Animation(v => CircleGraphicsView.Rotation = v, 0, 360);
        //animation.Commit(this, "SpinAnimation", 16, 2000, Easing.Linear, (v, c) => CircleGraphicsView.Rotation = 0, () => true);
    }
}