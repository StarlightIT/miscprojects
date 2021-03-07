import { Component, OnInit, OnChanges, AfterViewInit, SimpleChanges, ViewChild, ElementRef } from '@angular/core';
import {
  Colors, 
  OrgDiagram, 
  FamConfig, 
  FamDiagram,
  FamItemConfig,
  OrgConfig, 
  OrgItemConfig, 
  Enabled, 
  RenderingMode, 
  TemplateConfig, 
  Size, 
  Thickness, 
  PageFitMode, 
  LabelAnnotationConfig,
  UpdateMode
} from 'basicprimitives';
import { FileUploadService } from '../services/file-upload.service'; 

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnChanges, AfterViewInit {
  file: File = null;
  timer;
  control;
  companyResponse;
  i = 0;

  @ViewChild("diagram") diagram: ElementRef;

  constructor(private fileUploadService: FileUploadService) { }
  
  ngOnInit(): void {
    
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log("changes");
  }

  ngAfterViewInit(): void {
    
    


  }

  drawDiagram(companyResponse): void {
    console.log(companyResponse);
    let options = new FamConfig();
    let items = [];

    for (const company of companyResponse.companies) {
      let image = 'assets/images/photos/c.png';

      if (company.parentCompanies.length !== 0 && company.subsidiaries.length !== 0) {
        image = 'assets/images/photos/b.png';
      }
      else if (company.parentCompanies.length === 0 && company.subsidiaries.length !== 0) {
        image = 'assets/images/photos/p.png';
      }
      else if (company.parentCompanies.length !== 0 && company.subsidiaries.length === 0) {
        image = 'assets/images/photos/s.png';
      }

      let item = new FamItemConfig({
        id: company.id,
        parents: company.parents,
        title: company.companyName,
        description: company.organizationalNumber,
        internalCompanyName: company.internalCompanyName,
        image: image
      });

      items.push(item);
    }

    let annotations = [];

    for (const annotation of companyResponse.annotations) {
      let labelAnnotation = new LabelAnnotationConfig({
        fromItem: annotation.from,
        toItems: annotation.to,
        title: annotation.percentage
      });

      annotations.push(labelAnnotation);
    }

    options.annotations = annotations;
    options.items = items;
    options.hasSelectorCheckbox = Enabled.False;

    options.pageFitMode = PageFitMode.AutoSize;
    options.showFrame = true;

    options.templates = [this.getInfoTemplate()];
    options.onItemRender = this.onTemplateRender;
    options.defaultTemplateName = "info";

    if (!this.control) {
      this.control = new FamDiagram(this.diagram.nativeElement, options);
    }
    else {
      this.control.update(UpdateMode.Recreate, options);
    }
  }

  onTemplateRender(event, data) {
    switch (data.renderingMode) {
      case RenderingMode.Create:
        /* Initialize template content here */
        break;
      case RenderingMode.Update:
        /* Update template content here */
        break;
    }

    var itemConfig = data.context;
    var element = data.element;

    if (data.templateName == "info") {
      //console.log(element);
      /*var photo = element.childNodes[1].firstChild;
      photo.src = itemConfig.image;
      photo.alt = itemConfig.title;*/

      var titleBackground = element.firstChild;
      titleBackground.style.backgroundColor = itemConfig.itemTitleColor || Colors.RoyalBlue;

      var title = element.firstChild.firstChild;
      title.textContent = itemConfig.title;

      var description = element.childNodes[1];
      description.textContent = itemConfig.description;

      var internalCompanyName = element.childNodes[2]
      internalCompanyName.textContent = itemConfig.internalCompanyName;
    }
  }

  getInfoTemplate() {
    var result = new TemplateConfig();
    result.name = "info";
    
    result.itemSize = new Size(118, 60);
    //result.minimizedItemSize = new Size(3, 3);
    //result.highlightPadding = new Thickness(4, 4, 4, 4);

    result.itemTemplate =["div",
      {
        "style": {
          width: result.itemSize.width + "px",
          height: result.itemSize.height + "px"
        },
        "class": ["bp-item", "bp-corner-all", "bt-item-frame"]
      },
      ["div",
        {
          "name": "titleBackground",
          "class": ["bp-item", "bp-corner-all", "bt-title-frame"],
          "style": {
            top: "2px",
            left: "2px",
            width: "112px",
            height: "20px"
          }
        },
        ["div",
          {
            "name": "title",
            "class": ["bp-item", "bp-title"],
            "style": {
              top: "3px",
              left: "6px",
            }
          }
        ]
      ],
      ["div",
        {
          "name": "description",
          "class": "bp-item",
          "style": {
            top: "22px",
            left: "6px",
            fontSize: "12px"
          }
        }
      ],
      ["div",
        {
          "name": "description",
          "class": "bp-item",
          "style": {
            top: "36px",
            left: "6px",
            fontSize: "12px"
          }
        }
      ]
    ];

    return result;
  }


  onChange(event) { 
    this.file = event.target.files[0]; 
  }
  
  onUpload() { 
    console.log(this.file); 
    this.fileUploadService.upload(this.file).subscribe(data => {
      this.companyResponse = data;
      this.drawDiagram(this.companyResponse);
    }); 
  } 

  reload() {
    window.location.reload();
  }

}
