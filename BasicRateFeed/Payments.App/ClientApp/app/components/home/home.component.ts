import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    private rateFeeds: RateFeedData[] = [];
    private currencyPair: string;
    private rate: number;
    
    ngOnInit() {
        this.hubConnection = new HubConnection('http://localhost:54267/rates-feed-hub');
        
        this.hubConnection.on('Send', (data: any) => {
            this.currencyPair = data.BaseCurrency + '/' + data.TargetCurrency;
            this.rate = data.RateValue;
            
            this.rateFeeds.push(data);
        });

        this.hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });
    }

}

export class RateFeedData {
    public BaseCurrency: string;
    public TargetCurrency: string;
    public RateValue: number;
    public Reference: string;

    constructor(private baseCurrency: string, private targetCurrency: string, private rateValue: number) {
        this.BaseCurrency = baseCurrency;
        this.TargetCurrency = targetCurrency;
        this.RateValue = rateValue;
    }
}
