import {Routes} from '@angular/router';
import {VideoFromComponent} from './views/video-form/video-form.component';
import {ListVideoComponent} from './views/list-video/list-video.component';

export const videoRoute: Routes = [
  {
    path: 'overview', component: ListVideoComponent
  },
  {
    path: 'new', component: VideoFromComponent
  },
  {
    path: ':videoId', component: VideoFromComponent
  }
]

export default videoRoute;
