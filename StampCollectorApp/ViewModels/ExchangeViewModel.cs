using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using StampCollectorApp.Services;
using System.Collections.ObjectModel;


namespace StampCollectorApp.ViewModels;

public partial class ExchangeViewModel : ObservableObject
{
    private readonly IExchangeService _exchangeService;
    private readonly IStampService _stampService;

    [ObservableProperty] ObservableCollection<Stamp> stampsForExchange = new();
    [ObservableProperty] ObservableCollection<StampExchange> exchanges = new();
    [ObservableProperty] Stamp? selectedStamp;
    [NotifyCanExecuteChangedFor(nameof(RegisterExchangeCommand))]


    [ObservableProperty] string collectorName = string.Empty;
    [NotifyCanExecuteChangedFor(nameof(RegisterExchangeCommand))]
    [ObservableProperty] string collectorContact = string.Empty;
    [NotifyCanExecuteChangedFor(nameof(RegisterExchangeCommand))]

    [ObservableProperty] string notes = string.Empty;



    public ExchangeViewModel(IExchangeService exchangeService, IStampService stampService)
    {
        _exchangeService = exchangeService;
        _stampService = stampService;
    }

    [RelayCommand]
    public async Task LoadStampsForExchange()
    {
        var result = await _stampService.GetStampsAsync(null, true);
        StampsForExchange = new ObservableCollection<Stamp>(result);
    }

    [RelayCommand]
    public async Task LoadExchanges()
    {
        var result = await _exchangeService.GetAllAsync();
        Exchanges = new ObservableCollection<StampExchange>(result);
    }

    [RelayCommand(CanExecute = nameof(CanRegisterExchange))]
    public async Task RegisterExchangeAsync()
    {
        if (SelectedStamp == null) return;

        var desc = $"{SelectedStamp.Name} ({SelectedStamp.Year}) - País: {SelectedStamp.CountryId}";

        var exchange = new StampExchange
        {
            StampDescription = desc,
            ExchangeDate = DateTime.Now,
            CollectorName = CollectorName,
            CollectorContact = CollectorContact,
            Notes = Notes
        };

        await _exchangeService.AddExchangeAsync(exchange);
        await _stampService.DeleteStampAsync(SelectedStamp);

        SelectedStamp = null;
        CollectorName = string.Empty;
        CollectorContact = string.Empty;
        Notes = string.Empty;

        await LoadStampsForExchange();
        await LoadExchanges();

        await Shell.Current.DisplayAlert(
            AppResources.TituloOperacaoComSucess,
            AppResources.MensagemAposTroca,
            "OK"
);
    }

    public bool CanRegisterExchange() =>
        SelectedStamp != null &&
        !string.IsNullOrWhiteSpace(CollectorName) &&
        !string.IsNullOrWhiteSpace(CollectorContact);
}