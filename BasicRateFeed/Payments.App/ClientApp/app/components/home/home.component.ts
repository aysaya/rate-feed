import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    private messages: RateFeedData[] = [];
    
    public sendMessage(): void {
        const data = new RateFeedData('CAD','USD',0.75);

        this.hubConnection.invoke('Send', data);
        this.messages.push(data);
    }

    ngOnInit() {
        this.hubConnection = new HubConnection('http://localhost:54267/rates-feed-hub');
        
        this.hubConnection.on('Send', (data: any) => {
            console.log(data);
            this.messages.push(data);
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
    public RateValue: any;

    constructor(private baseCurrency: string, private targetCurrency: string, private rateValue: any) {
        this.BaseCurrency = baseCurrency;
        this.TargetCurrency = targetCurrency;
        this.RateValue = rateValue;
    }
}
