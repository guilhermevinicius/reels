import {Component} from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {Button} from 'primeng/button';
import {VideoService} from '../../services';

@Component({
  selector: 'list-video',
  templateUrl: './list-video.component.html',
  imports: [
    RouterModule,
    Button
  ]
})
export class ListVideoComponent {

  videos = [1,2,3,4]

  constructor(
    private router: Router
  ) { }

  goTo() {
    this.router.navigateByUrl('/videos/new');
  }

}
