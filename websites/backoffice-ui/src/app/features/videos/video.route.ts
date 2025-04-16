import {Routes} from '@angular/router';
import {VideoFromComponent} from './views/video-form/video-form.component';
import {ListVideoComponent} from './views/list-video/list-video.component';

export const videoRoute: Routes = [
  {
    path: '', component: ListVideoComponent
  },
  {
    path: 'new', component: VideoFromComponent
  },
  {
    path: ':id', component: VideoFromComponent
  }
]

export default videoRoute;
