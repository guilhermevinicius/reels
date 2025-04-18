import {Component, OnInit} from '@angular/core';
import {Router, RouterModule} from '@angular/router';
import {VideoService} from '../../services';
import {IVideo} from '../../models';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'list-video',
  templateUrl: './list-video.component.html',
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class ListVideoComponent implements OnInit {
  videos: IVideo[] = []

  constructor(
    private videoService: VideoService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.videoService.listVideo$().subscribe({
      next: data => {
        this.videos = data.data
      },
      error: err => {}
    })
  }

  goTo() {
    this.router.navigateByUrl('/videos/new');
  }

}
