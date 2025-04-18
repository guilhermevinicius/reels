import {Component} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ActivatedRoute, Router, RouterModule} from '@angular/router';
import {UploadDraggingDropComponent} from '../../components/uploder-drag-drop/upload-drag-drop.component';
import {InputComponent, SelectInputComponent, TextAreaInputComponent} from '../../../../components';
import {VideoService} from '../../services';
import {IVideo, IVideoRequest} from '../../models';
import {CommonModule} from '@angular/common';
import {addWarning} from '@angular-devkit/build-angular/src/utils/webpack-diagnostics';

@Component({
  selector: 'video-form',
  templateUrl: './video-form.component.html',
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    UploadDraggingDropComponent,
    InputComponent,
    SelectInputComponent,
    TextAreaInputComponent,
  ]
})
export class VideoFromComponent {
  thumb: File | null = null;
  thumbHalf: File | null = null;

  previewThumb: string | null = null;
  previewThumbHalf: string | null = null;

  videoForm = new FormGroup({
    title: new FormControl<string | null>(null, Validators.required),
    description: new FormControl<string | null>(null, Validators.required),
    yearLaunched: new FormControl<number | null>(null, Validators.required),
    duration: new FormControl<number | null>(null, Validators.required),
    rating: new FormControl<number | null>(null, Validators.required),
    category: new FormControl<string | null>(null, Validators.required)
  })

  ratings = [
    {name: 'ER', value: 0},
    {name: 'L', value: 1},
    {name: 'Rate10', value: 2},
    {name: 'Rate12', value: 3},
    {name: 'Rate14', value: 4},
    {name: 'Rate16', value: 5},
    {name: 'Rate18', value: 6}
  ]

  categories = [
    { name: 'category', value: 'category' },
    { name: 'category', value: 'category' }
  ]

  types = [
    { name: 'Film', value: 1 },
    { name: 'Series', value: 2 }
  ]

  constructor(
    private videoService: VideoService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    activatedRoute.params.subscribe((params: any) => {
      if (params.videoId) {
        this.videoService.getVideo$(params.videoId).subscribe({
          next: data => {
            this.setForm(data.data)
          },
          error: params => {},
        })
      }
    })
  }

  setForm(video: IVideo): void {
    this.videoForm.setValue({
      title: video.title,
      description: video.description,
      yearLaunched: video.yearLaunched,
      duration: video.duration,
      rating: video.rating,
      category: ""
    });

    this.previewThumb = video.thumb;
    this.previewThumbHalf = video.thumbHalf;
  }

  onSubmit(): void {
    const fields = this.videoForm.getRawValue();

    if (this.thumb === null && this.thumbHalf === null)
      return;

    const formData = new FormData()
    formData.append('title', fields.title!)
    formData.append('description', fields.description!)
    formData.append('yearLaunched', fields.yearLaunched!.toString())
    formData.append('duration', fields.duration!.toString())
    formData.append('rating', fields.rating!.toString())
    formData.append('thumb', this.thumb!)
    formData.append('thumbHalf', this.thumbHalf!)

    // thumb: File | null = null;
    // thumbHalf: File | null = null;

    this.videoService.createVideo$(formData).subscribe({
      next: params => {
        this.router.navigateByUrl('/videos/overview')
      },
      error: err => {
      }
    })
  }

}
