public void ModelLineToRoomBoundary()
{

	UIDocument uidoc = this.ActiveUIDocument;
	Document doc = uidoc.Document;
			
	View activeView = doc.ActiveView;

	CurveArray curves = new CurveArray();

	Selection sel = uidoc.Selection;
	PickFilter selFilter = new PickFilter();
	IList<Element> pickedRef = sel.PickElementsByRectangle(selFilter,"Select lines");
						
    foreach (Element e in pickedRef)
	{
				
		ModelCurve m = e as ModelCurve;
		Curve c = m.GeometryCurve;
		curves.Append(c);
	}

	using (Transaction transaction = new Transaction(doc))
	{
		transaction.Start("Create Rooms");
		doc.Create.NewRoomBoundaryLines(activeView.SketchPlane, curves, activeView);
		transaction.Commit();
	}	
}
		
public class PickFilter : ISelectionFilter
{
    public bool AllowElement(Element e)
    {
        return (e.Category.Id.IntegerValue.Equals(
        (int)BuiltInCategory.OST_Lines));
    }
    public bool AllowReference(Reference r, XYZ p)
    {	
        return false;
    }
}