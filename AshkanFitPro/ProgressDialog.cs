using System;using System.Linq;using System.Windows.Forms;
namespace AshkanFitPro { public partial class ProgressDialog : Form {
readonly Repository repository;
public ProgressDialog(Repository repository){this.repository=repository;InitializeComponent();foreach(var member in repository.Data.Members.OrderBy(x=>x.Name))memberBox.Items.Add(new MemberItem(member));if(memberBox.Items.Count>0)memberBox.SelectedIndex=0;}
sealed class MemberItem {public readonly Member Value;public MemberItem(Member value){Value=value;}public override string ToString(){return Value.Name+" — "+Value.Phone;}}
void SaveClicked(object sender,EventArgs e){var selected=memberBox.SelectedItem as MemberItem;if(selected==null){MessageBox.Show("Please select a member / لطفاً عضو را انتخاب کنید");return;}if(weightBox.Value<=0||heightBox.Value<=0){MessageBox.Show("Weight and height must be positive / وزن و قد باید مثبت باشند");return;}
var item=new BodyProgress{Id=Guid.NewGuid(),MemberId=selected.Value.Id,RecordedAt=dateBox.Value.Date,WeightKg=weightBox.Value,HeightCm=heightBox.Value,WaistCm=waistBox.Value,BodyFatPercent=fatBox.Value,Goal=goalBox.Text.Trim()};repository.Data.BodyProgressRecords.Add(item);try{repository.Save();MessageBox.Show("Saved. BMI: "+item.Bmi.ToString("0.0"));DialogResult=DialogResult.OK;Close();}catch(Exception ex){repository.Data.BodyProgressRecords.Remove(item);MessageBox.Show(ex.Message);}}
} }
