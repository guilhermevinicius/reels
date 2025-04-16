import {Component} from '@angular/core';
import {Tab, TabList, Tabs} from 'primeng/tabs';
import {RouterModule} from '@angular/router';
import {Toast} from 'primeng/toast';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  imports: [
    Tabs,
    TabList,
    Tab,
    RouterModule,
    Toast
  ]
})
export class MainLayoutComponent {
}
