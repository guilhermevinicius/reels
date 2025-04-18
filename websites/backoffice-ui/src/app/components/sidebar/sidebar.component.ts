import {Component} from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  imports: [
    RouterLink,
    RouterLinkActive
  ]
})
export class SidebarComponent {

  menus = [
    {
      label: 'Videos', icon: 'pi pi-play-circle', subMenus: [
        {label: 'Overview', link: 'videos/overview'},
        {label: 'Novo video', link: 'videos/new'}
      ]
    },
    {
      label: 'Settings', icon: 'pi pi-cog', subMenus: [
        {label: 'Categories', link: 'settings/category'},
        {label: 'Genre', link: 'settings/genre'},
      ]
    },
  ]

}
