import {Component} from '@angular/core';
import {RouterModule} from '@angular/router';
import {SidebarComponent} from '../../../../components';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  imports: [
    RouterModule,
    SidebarComponent
  ]
})
export class MainLayoutComponent {
}
