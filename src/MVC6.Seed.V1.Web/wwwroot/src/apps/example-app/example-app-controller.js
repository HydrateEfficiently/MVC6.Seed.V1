import { Injectable } from './../../utility/injectable';
import * as arrayUtility from './../../utility/array-utility';
import * as guidUtility from './../../utility/guid-utility';

export class CreateJobController extends Injectable {
    static get $inject() {
        return ['AngularComponentNamePrefixActorAttributeService', 'AngularComponentNamePrefixWorkItemService', 'AngularComponentNamePrefixServerData'];
    }

    constructor(...deps) {
        super(...deps);

        this.selectedAge = null;
        this.selectedGender = null;
        this.selectedLanguage = null;

        this.genders = this.AngularComponentNamePrefixActorAttributeService.getGenders();
        this.ages = this.AngularComponentNamePrefixActorAttributeService.getAges();
        this.purposes = this.AngularComponentNamePrefixActorAttributeService.getExpertise();
        this.ratings = [3, 3.5, 4, 4.5];

        let self = this;
        if (!guidUtility.isEmpty(this.AngularComponentNamePrefixServerData.workItemId)) {
            this.AngularComponentNamePrefixWorkItemService.get(this.AngularComponentNamePrefixServerData.workItemId)
                .then(workItem => {
                    if (!self.ratings.includes(workItem.minimumRating)) {
                        workItem.minimumRating = self.ratings[0];
                    }
                    self.genders = arrayUtility.except(self.genders, workItem.genderRequirements);
                    self.ages = arrayUtility.except(self.ages, workItem.ageRequirements);
                    self.workItem = workItem;
                });
        } else {
            this.workItem = {
                minimumRating: self.ratings[0],
                genderRequirements: [],
                ageRequirements: [],
                languageRequirements: []
            };
        }
    }

    addAge() {
        arrayUtility.swap(this.ages, this.workItem.ageRequirements, this.selectedAge);
        this.selectedAge = null;
    }

    removeAge(age) {
        arrayUtility.swap(this.workItem.ageRequirements, this.ages, age);
    }

    addGender() {
        arrayUtility.swap(this.genders, this.workItem.genderRequirements, this.selectedGender);
        this.selectedGender = null;
    }

    removeGender(gender) {
        arrayUtility.swap(this.workItem.genderRequirements, this.genders, gender);
    }

    addLanguage() {
    }

    removeLanguage(language) {
        arrayUtility.remove(this.workItem.languageRequirements, language);
    }

    saveDraft() {
        this.AngularComponentNamePrefixWorkItemService.saveDraft(this.workItem);
    }
}