import {Routes} from '@angular/router';

export const mainRoute: Routes = [
  {
    path: 'settings',
    loadChildren: () => import('../settings/setting.route')
  },
  {
    path: 'videos',
    loadChildren: () => import('../videos/video.route')
  },
  {path: '**', redirectTo: 'videos/overview'},
]

export default mainRoute;
