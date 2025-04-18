import {Component, EventEmitter, Input, OnChanges, Output, SimpleChanges} from '@angular/core';

@Component({
  selector: 'video-upload-drag-drop',
  templateUrl: './upload-drag-drop.component.html'
})
export class UploadDraggingDropComponent implements OnChanges {
  @Input() previewUrl: string | null = null;
  @Input({required: true}) title: string | null = null;
  @Output() file = new EventEmitter<File>();

  filePath: File | null = null;
  previewPath: string | null = null;

  ngOnChanges(changes: SimpleChanges): void {
    if (this.previewUrl)
      this.previewPath = this.previewUrl
  }

  onDragOver(event: Event): void {
  }

  onDrop(event: Event): void {
    console.log('onDrop', event)
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files?.length) {
      const reader = new FileReader();
      this.filePath = input.files[0]
      reader.onload = () => {
        this.previewPath = reader.result as string;
      };
      reader.readAsDataURL(this.filePath);
      this.file.emit(this.filePath)
    }
  }

  cleanFile() {
    this.filePath = null;
    this.previewPath = null;
  }
}
