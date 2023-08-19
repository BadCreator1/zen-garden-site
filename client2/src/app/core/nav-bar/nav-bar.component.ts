import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/IUser';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit{
  public isCollapsed = true;
  public apiUrl = environment.apiUrl.replace('/api','');
  currentUser$?: Observable<IUser | null>;

  constructor(private accountService: AccountService){

  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    console.log(this.currentUser$);
  }

  logout(){
    this.accountService.logout();
  }
  
  public handleMissingImage(event: Event) {
    console.log("see error");
    (event.target as HTMLImageElement).style.display = 'none';
  }
}
